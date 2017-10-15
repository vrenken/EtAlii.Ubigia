namespace EtAlii.Servus.Infrastructure.Fabric
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Servus.Api;
    using EtAlii.xTechnology.Logging;

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

        public void Update(IEditableEntry entry, IEnumerable<IComponent> changedComponents)
        {
            Update((Entry)entry, changedComponents);
        }

        public void Update(Entry entry, IEnumerable<IComponent> changedComponents)
        {
            EnsureModelConsistensy(entry, changedComponents);
        }

        private void EnsureModelConsistensy(Entry entry, IEnumerable<IComponent> components)
        {
            EnsureFirstTypeHierarchicalConsistency(entry, components);
            EnsureSecondTypeHierarchicalConsistency(entry, components);
            EnsureSequentialConsistency(entry, components);
            EnsureTransformationalConsistency(entry, components);
            EnsureIndexedConsistency(entry, components);
        }

        private void EnsureSequentialConsistency(Entry entry, IEnumerable<IComponent> components)
        {
            var previousComponent = components.OfType<PreviousComponent>()
                                              .SingleOrDefault();
            if (previousComponent != null)
            {
                var previousId = previousComponent.Relation.Id;
                if (previousId != Identifier.Empty)
                {
                    var previous = (IEditableEntry)_entryGetter.Get(previousId, EntryRelation.Previous | EntryRelation.Next);
                    if (previous.Next == Relation.None)
                    {
                        //_logger.Verbose("Updating entry - Adding relation from previous to next: {0} => {1}", previousId.ToTimeString(), entry.Id.ToTimeString());
                        previous.Next = Relation.NewRelation(entry.Id);
                        _entryStorer.Store(previous);
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

        private void EnsureTransformationalConsistency(Entry entry, IEnumerable<IComponent> components)
        {
            var downdateComponent = components.OfType<DowndateComponent>()
                                              .SingleOrDefault();
            if (downdateComponent != null)
            {
                var downdateId = downdateComponent.Relation.Id;
                if (downdateId != Identifier.Empty)
                {
                    var downdate = (IEditableEntry)_entryGetter.Get(downdateId, EntryRelation.Downdate | EntryRelation.Update);
                    if (!downdate.Updates.Contains(entry.Id)) 
                    {
                        //_logger.Verbose("Updating entry - Adding relation from downdate to update: {0} => {1}", downdateId.ToTimeString(), entry.Id.ToTimeString());
                        downdate.Updates.Add(entry.Id);
                        _entryStorer.Store(downdate);
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

        private void EnsureFirstTypeHierarchicalConsistency(Entry entry, IEnumerable<IComponent> components)
        {
            var parentComponent = components.OfType<ParentComponent>()
                                            .SingleOrDefault();
            if (parentComponent != null)
            {
                var parentId = parentComponent.Relation.Id;
                if (parentId != Identifier.Empty)
                {
                    var parent = (IEditableEntry)_entryGetter.Get(parentId, EntryRelation.Parent | EntryRelation.Child);
                    if (!parent.Children.Contains(entry.Id))
                    {
                        //_logger.Verbose("Updating entry - Adding first type hierarchical relation from parent to child: {0} => {1}", parentId.ToTimeString(), entry.Id.ToTimeString());
                        parent.Children.Add(entry.Id);
                        _entryStorer.Store(parent);
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

        private void EnsureSecondTypeHierarchicalConsistency(Entry entry, IEnumerable<IComponent> components)
        {
            var parent2Component = components.OfType<Parent2Component>()
                                             .SingleOrDefault();
            if (parent2Component != null)
            {
                var parent2Id = parent2Component.Relation.Id;
                if (parent2Id != Identifier.Empty)
                {
                    var parent2 = (IEditableEntry)_entryGetter.Get(parent2Id, EntryRelation.Parent | EntryRelation.Child);
                    if (!parent2.Children2.Contains(entry.Id))
                    {
                        //_logger.Verbose("Updating entry - Adding second type hierarchical relation from parent to child: {0} => {1}", parent2Id.ToTimeString(), entry.Id.ToTimeString());
                        parent2.Children2.Add(entry.Id);
                        _entryStorer.Store(parent2);
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

        private void EnsureIndexedConsistency(Entry entry, IEnumerable<IComponent> components)
        {
            EnsuredIndexedConsistencyFromIndexedToIndex(entry, components);
            EnsuredIndexedConsistencyFromIndexToIndexed(entry, components);
        }

        private void EnsuredIndexedConsistencyFromIndexToIndexed(Entry entry, IEnumerable<IComponent> components)
        {
            var indexesComponents = components.OfType<IndexesComponent>();
            foreach (var indexesComponent in indexesComponents)
            {
                foreach (var indexesRelation in indexesComponent.Relations)
                {
                    var indexId = indexesRelation.Id;
                    if (indexId != Identifier.Empty)
                    {
                        var index = (IEditableEntry) _entryGetter.Get(indexId, EntryRelation.Index | EntryRelation.Indexed);
                        if (index.Indexed == Relation.None)
                        {
                            //_logger.Verbose("Updating entry - Adding relation from index to indexed: {0} => {1}", entry.Id.ToTimeString(), indexId.ToTimeString());
                            index.Indexed = Relation.NewRelation(entry.Id);
                            _entryStorer.Store(index);
                        }
                        if (index.Indexed.Id == entry.Id)
                        {
                            // No actions needed, the relation is already in place.
                        }
                        else
                        {
                            throw new SpaceFabricException(entry.Id, indexId,
                                "Unable to add index relation from index to indexed: index.Indexed is already in use");
                        }
                    }
                    else
                    {
                        throw new SpaceFabricException(entry.Id, indexId,
                            "Unable to create index relation from index to indexed: indexId cannot be Identifier.Empty");
                    }
                }
            }
        }

        private void EnsuredIndexedConsistencyFromIndexedToIndex(Entry entry, IEnumerable<IComponent> components)
        {
            var indexedComponent = components.OfType<IndexedComponent>()
                .SingleOrDefault();
            if (indexedComponent != null)
            {
                var indexedId = indexedComponent.Relation.Id;
                if (indexedId != Identifier.Empty)
                {
                    var indexed = (IEditableEntry) _entryGetter.Get(indexedId, EntryRelation.Index | EntryRelation.Indexed);
                    if (!indexed.Indexes.Contains(entry.Id))
                    {
                        //_logger.Verbose("Updating entry - Adding relation from indexed to index: {0} => {1}", indexedId.ToTimeString(), entry.Id.ToTimeString());
                        indexed.Indexes.Add(entry.Id);
                        _entryStorer.Store(indexed);
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