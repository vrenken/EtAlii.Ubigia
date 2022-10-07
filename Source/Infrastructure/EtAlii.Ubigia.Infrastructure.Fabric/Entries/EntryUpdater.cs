// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class EntryUpdater : IEntryUpdater
    {
        private readonly IEntryStorer _entryStorer;
        private readonly IEntryGetter _entryGetter;

        public EntryUpdater(
            IEntryStorer entryStorer,
            IEntryGetter entryGetter)
        {
            _entryStorer = entryStorer;
            _entryGetter = entryGetter;
        }

        /// <inheritdoc />
        public Task Update(IEditableEntry entry, IEnumerable<IComponent> changedComponents)
        {
            return Update((Entry)entry, changedComponents);
        }

        /// <inheritdoc />
        public Task Update(Entry entry, IEnumerable<IComponent> changedComponents)
        {
            return EnsureModelConsistency(entry, changedComponents);
        }

        private async Task EnsureModelConsistency(Entry entry, IEnumerable<IComponent> components)
        {
            await EnsureFirstTypeHierarchicalConsistency(entry, components).ConfigureAwait(false);
            await EnsureSecondTypeHierarchicalConsistency(entry, components).ConfigureAwait(false);
            await EnsureSequentialConsistency(entry, components).ConfigureAwait(false);
            await EnsureTransformationalConsistency(entry, components).ConfigureAwait(false);
            await EnsureIndexedConsistency(entry, components).ConfigureAwait(false);
        }

        private async Task EnsureSequentialConsistency(Entry entry, IEnumerable<IComponent> components)
        {
            var previousComponent = components.OfType<PreviousComponent>()
                                              .SingleOrDefault();
            if (previousComponent != null)
            {
                var previousId = previousComponent.Relation.Id;
                if (previousId != Identifier.Empty)
                {
                    var previous = (IEditableEntry) await _entryGetter.Get(previousId, EntryRelations.Previous | EntryRelations.Next).ConfigureAwait(false);
                    if (previous.Next == Relation.None)
                    {
                        //_logger.Verbose("Updating entry - Adding relation from previous to next: [0] => [1]", previousId.ToTimeString(), entry.Id.ToTimeString())
                        previous.Next = Relation.NewRelation(entry.Id);
                        await _entryStorer.Store(previous).ConfigureAwait(false);
                    }
                    else
                    {
                        throw new SpaceFabricException(entry.Id, previousId, "Unable to create sequential relation: previous.Next is already in use");
                    }
                }
                else
                {
                    throw new SpaceFabricException(entry.Id, previousId, "Unable to create sequential relation: source.Previous cannot be Identifier.Empty");
                }
            }
        }

        private async Task EnsureTransformationalConsistency(Entry entry, IEnumerable<IComponent> components)
        {
            var downdateComponent = components
                .OfType<DowndateComponent>()
                .SingleOrDefault();
            if (downdateComponent != null)
            {
                var downdateId = downdateComponent.Relation.Id;
                if (downdateId != Identifier.Empty)
                {
                    var downdate = (IEditableEntry)await _entryGetter.Get(downdateId, EntryRelations.Downdate | EntryRelations.Update).ConfigureAwait(false);
                    if (!downdate.Updates.Contains(entry.Id))
                    {
                        //_logger.Verbose("Updating entry - Adding relation from downdate to update: [0] => [1]", downdateId.ToTimeString(), entry.Id.ToTimeString())
                        downdate.AddUpdate(entry.Id);
                        await _entryStorer.Store(downdate).ConfigureAwait(false);
                    }
                    else
                    {
                        throw new SpaceFabricException(entry.Id, downdateId, "Unable to add transformational relation: downdate.Updates already relates to update");
                    }
                }
                else
                {
                    throw new SpaceFabricException(entry.Id, downdateId, "Unable to create transformational relation: source.Downdate cannot be Identifier.Empty");
                }
            }
        }

        private async Task EnsureFirstTypeHierarchicalConsistency(Entry entry, IEnumerable<IComponent> components)
        {
            var parentComponent = components.OfType<ParentComponent>()
                                            .SingleOrDefault();
            if (parentComponent != null)
            {
                var parentId = parentComponent.Relation.Id;
                if (parentId != Identifier.Empty)
                {
                    var parent = (IEditableEntry)await _entryGetter.Get(parentId, EntryRelations.Parent | EntryRelations.Child).ConfigureAwait(false);
                    if (!parent.Children.Contains(entry.Id))
                    {
                        //_logger.Verbose("Updating entry - Adding first type hierarchical relation from parent to child: [0] => [1]", parentId.ToTimeString(), entry.Id.ToTimeString())
                        parent.Children.Add(entry.Id);
                        await _entryStorer.Store(parent).ConfigureAwait(false);
                    }
                    else
                    {
                        throw new SpaceFabricException(entry.Id, parentId, "Unable to add first type hierarchical relation: parent.Children already relates to child");
                    }
                }
                else
                {
                    throw new SpaceFabricException(entry.Id, parentId, "Unable to create first type hierarchical relation: source.Parent cannot be Identifier.Empty");
                }
            }
        }

        private async Task EnsureSecondTypeHierarchicalConsistency(Entry entry, IEnumerable<IComponent> components)
        {
            var parent2Component = components.OfType<Parent2Component>()
                                             .SingleOrDefault();
            if (parent2Component != null)
            {
                var parent2Id = parent2Component.Relation.Id;
                if (parent2Id != Identifier.Empty)
                {
                    var parent2 = (IEditableEntry)await _entryGetter.Get(parent2Id, EntryRelations.Parent | EntryRelations.Child).ConfigureAwait(false);
                    if (!parent2.Children2.Contains(entry.Id))
                    {
                        //_logger.Verbose("Updating entry - Adding second type hierarchical relation from parent to child: [0] => [1]", parent2Id.ToTimeString(), entry.Id.ToTimeString())
                        parent2.Children2.Add(entry.Id);
                        await _entryStorer.Store(parent2).ConfigureAwait(false);
                    }
                    else
                    {
                        throw new SpaceFabricException(entry.Id, parent2Id, "Unable to add second type hierarchical relation: parent.Children2 already relates to child");
                    }
                }
                else
                {
                    throw new SpaceFabricException(entry.Id, parent2Id, "Unable to create second type hierarchical relation: source.Parent2 cannot be Identifier.Empty");
                }
            }
        }

        private async Task EnsureIndexedConsistency(Entry entry, IEnumerable<IComponent> components)
        {
            await EnsuredIndexedConsistencyFromIndexedToIndex(entry, components).ConfigureAwait(false);
            await EnsuredIndexedConsistencyFromIndexToIndexed(entry, components).ConfigureAwait(false);
        }

        private async Task EnsuredIndexedConsistencyFromIndexToIndexed(Entry entry, IEnumerable<IComponent> components)
        {
            var indexesComponents = components.OfType<IndexesComponent>();
            foreach (var indexesComponent in indexesComponents)
            {
                var indexesRelationIds = indexesComponent.Relations
                    .Select(relation => relation.Id)
                    .ToArray();
                foreach (var indexesRelationId in indexesRelationIds)
                {
                    if (indexesRelationId != Identifier.Empty)
                    {
                        var index = (IEditableEntry)await _entryGetter.Get(indexesRelationId, EntryRelations.Index | EntryRelations.Indexed).ConfigureAwait(false);
                        if (index.Indexed == Relation.None)
                        {
                            //_logger.Verbose("Updating entry - Adding relation from index to indexed: [0] => [1]", entry.Id.ToTimeString(), indexId.ToTimeString())
                            index.Indexed = Relation.NewRelation(entry.Id);
                            await _entryStorer.Store(index).ConfigureAwait(false);
                        }
                        if (index.Indexed.Id != entry.Id)
                        {
                            throw new SpaceFabricException(entry.Id, indexesRelationId, "Unable to add index relation from index to indexed: index.Indexed is already in use");
                        }
                    }
                    else
                    {
                        throw new SpaceFabricException(entry.Id, indexesRelationId,
                            "Unable to create index relation from index to indexed: indexId cannot be Identifier.Empty");
                    }
                }
            }
        }

        private async Task EnsuredIndexedConsistencyFromIndexedToIndex(Entry entry, IEnumerable<IComponent> components)
        {
            var indexedComponent = components.OfType<IndexedComponent>()
                .SingleOrDefault();
            if (indexedComponent != null)
            {
                var indexedId = indexedComponent.Relation.Id;
                if (indexedId != Identifier.Empty)
                {
                    var indexed = (IEditableEntry)await _entryGetter.Get(indexedId, EntryRelations.Index | EntryRelations.Indexed).ConfigureAwait(false);
                    if (!indexed.Indexes.Contains(entry.Id))
                    {
                        //_logger.Verbose("Updating entry - Adding relation from indexed to index: [0] => [1]", indexedId.ToTimeString(), entry.Id.ToTimeString())
                        indexed.Indexes.Add(entry.Id);
                        await _entryStorer.Store(indexed).ConfigureAwait(false);
                    }
                    else
                    {
                        throw new SpaceFabricException(entry.Id, indexedId,
                            "Unable to add index relation from indexed to index: indexed.Indexes already relates to indexed");
                    }
                }
                else
                {
                    throw new SpaceFabricException(entry.Id, indexedId,
                        "Unable to create index relation from indexed to index: indexedId cannot be Identifier.Empty");
                }
            }
        }
    }
}
