// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence;

    internal class EntryGetter : IEntryGetter
    {
        private readonly IStorage _storage;

        public EntryGetter(IStorage storage)
        {
            _storage = storage;
        }

        public async IAsyncEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelations entryRelations)
        {
            foreach (var identifier in identifiers)
            {
                yield return await Get(identifier, entryRelations).ConfigureAwait(false);
            }
        }

        public async Task<Entry> Get(Identifier identifier, EntryRelations entryRelations)
        {
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);

            var selectedComponents = new List<IComponent>();

            var components = RetrieveAndAdd<IdentifierComponent>(containerId);
            await foreach(var component in components.ConfigureAwait(false))
            {
                selectedComponents.Add(component);
            }
            components = RetrieveAndAdd<TypeComponent>(containerId);
            await foreach(var component in components.ConfigureAwait(false))
            {
                selectedComponents.Add(component);
            }
            components = RetrieveAndAdd<TagComponent>(containerId);
            await foreach(var component in components.ConfigureAwait(false))
            {
                selectedComponents.Add(component);
            }

            // Previous
            components = RetrieveAndAdd<PreviousComponent>(containerId, entryRelations, EntryRelations.Previous);
            await foreach(var component in components.ConfigureAwait(false))
            {
                selectedComponents.Add(component);
            }

            // Next
            components = RetrieveAndAdd<NextComponent>(containerId, entryRelations, EntryRelations.Next);
            await foreach(var component in components.ConfigureAwait(false))
            {
                selectedComponents.Add(component);
            }

            // Update
            components = RetrieveAndAddAll<UpdatesComponent>(containerId, entryRelations, EntryRelations.Update);
            await foreach(var component in components.ConfigureAwait(false))
            {
                selectedComponents.Add(component);
            }

            // Downdate
            components = RetrieveAndAdd<DowndateComponent>(containerId, entryRelations, EntryRelations.Downdate);
            await foreach(var component in components.ConfigureAwait(false))
            {
                selectedComponents.Add(component);
            }

            // Parent
            components = RetrieveAndAdd<ParentComponent>(containerId, entryRelations, EntryRelations.Parent);
            await foreach(var component in components.ConfigureAwait(false))
            {
                selectedComponents.Add(component);
            }
            components = RetrieveAndAdd<Parent2Component>(containerId, entryRelations, EntryRelations.Parent);
            await foreach(var component in components.ConfigureAwait(false))
            {
                selectedComponents.Add(component);
            }

            // Child
            components = RetrieveAndAddAll<ChildrenComponent>(containerId, entryRelations, EntryRelations.Child);
            await foreach(var component in components.ConfigureAwait(false))
            {
                selectedComponents.Add(component);
            }
            components = RetrieveAndAddAll<Children2Component>(containerId, entryRelations, EntryRelations.Child);
            await foreach(var component in components.ConfigureAwait(false))
            {
                selectedComponents.Add(component);
            }

            // Index
            components = RetrieveAndAddAll<IndexesComponent>(containerId, entryRelations, EntryRelations.Index);
            await foreach(var component in components.ConfigureAwait(false))
            {
                selectedComponents.Add(component);
            }

            // Indexed
            components = RetrieveAndAdd<IndexedComponent>(containerId, entryRelations, EntryRelations.Indexed);
            await foreach(var component in components.ConfigureAwait(false))
            {
                selectedComponents.Add(component);
            }

            var entry = EntryHelper.Compose(selectedComponents, true);
            return entry;
        }

        public async IAsyncEnumerable<Entry> GetRelated(Identifier identifier, EntryRelations entriesWithRelation, EntryRelations entryRelations)
        {
            var entry = await Get(identifier, entriesWithRelation).ConfigureAwait(false);

            var entries = AddHierarchicalEntries(entriesWithRelation, entryRelations, entry);
            await foreach (var e in entries.ConfigureAwait(false))
            {
                yield return e;
            }

            entries = AddIndexEntries(entriesWithRelation, entryRelations, entry);
            await foreach (var e in entries.ConfigureAwait(false))
            {
                yield return e;
            }

            entries = AddSequentialEntries(entriesWithRelation, entryRelations, entry);
            await foreach (var e in entries.ConfigureAwait(false))
            {
                yield return e;
            }

            entries = AddTemporalEntries(entriesWithRelation, entryRelations, entry);
            await foreach (var e in entries.ConfigureAwait(false))
            {
                yield return e;
            }
        }

        private async IAsyncEnumerable<Entry> AddTemporalEntries(
            EntryRelations entriesWithRelation,
            EntryRelations entryRelations,
            IReadOnlyEntry entry)
        {
            if (entriesWithRelation.HasFlag(EntryRelations.Downdate) && entry.Downdate != Relation.None)
            {
                yield return await Get(entry.Downdate.Id, entryRelations).ConfigureAwait(false);
            }

            if (entriesWithRelation.HasFlag(EntryRelations.Update))
            {
                foreach (var update in entry.Updates)
                {
                    yield return await Get(update.Id, entryRelations).ConfigureAwait(false);
                }
            }
        }

        private async IAsyncEnumerable<Entry> AddSequentialEntries(
            EntryRelations entriesWithRelation,
            EntryRelations entryRelations,
            IReadOnlyEntry entry)
        {
            if (entriesWithRelation.HasFlag(EntryRelations.Previous) && entry.Previous != Relation.None)
            {
                yield return await Get(entry.Previous.Id, entryRelations).ConfigureAwait(false);
            }

            if (!entriesWithRelation.HasFlag(EntryRelations.Next) || entry.Next == Relation.None) yield break;
            yield return await Get(entry.Next.Id, entryRelations).ConfigureAwait(false);
        }

        private async IAsyncEnumerable<Entry> AddIndexEntries(
            EntryRelations entriesWithRelation,
            EntryRelations entryRelations,
            IReadOnlyEntry entry)
        {
            if (entriesWithRelation.HasFlag(EntryRelations.Index))
            {
                foreach (var index in entry.Indexes)
                {
                    yield return await Get(index.Id, entryRelations).ConfigureAwait(false);
                }
            }

            if (!entriesWithRelation.HasFlag(EntryRelations.Indexed) || entry.Indexed == Relation.None) yield break;
            yield return await Get(entry.Parent.Id, entryRelations).ConfigureAwait(false);
        }

        private async IAsyncEnumerable<Entry> AddHierarchicalEntries(
            EntryRelations entriesWithRelation,
            EntryRelations entryRelations,
            IReadOnlyEntry entry)
        {
            if (entriesWithRelation.HasFlag(EntryRelations.Parent))
            {
                if (entry.Parent != Relation.None)
                {
                    yield return await Get(entry.Parent.Id, entryRelations).ConfigureAwait(false);
                }

                if (entry.Parent2 != Relation.None)
                {
                    yield return await Get(entry.Parent2.Id, entryRelations).ConfigureAwait(false);
                }
            }

            if (!entriesWithRelation.HasFlag(EntryRelations.Child)) yield break;
            foreach (var child in entry.Children)
            {
                yield return await Get(child.Id, entryRelations).ConfigureAwait(false);
            }

            foreach (var child2 in entry.Children2)
            {
                yield return await Get(child2.Id, entryRelations).ConfigureAwait(false);
            }
        }

        private async IAsyncEnumerable<IComponent> RetrieveAndAddAll<T>(ContainerIdentifier containerId, EntryRelations entryRelations, EntryRelations entryRelationToMatch)
            where T : CompositeComponent
        {
            if (entryRelations.HasFlag(entryRelationToMatch))
            {
                var components = _storage.Components.RetrieveAll<T>(containerId);
                await foreach (var component in components.ConfigureAwait(false))
                {
                    yield return component;
                }
            }
        }

        private async IAsyncEnumerable<IComponent> RetrieveAndAdd<T>(ContainerIdentifier containerId)
            where T : NonCompositeComponent
        {
            var component = await _storage.Components.Retrieve<T>(containerId).ConfigureAwait(false);
            if (component != null)
            {
                yield return component;
            }
        }

        private async IAsyncEnumerable<IComponent> RetrieveAndAdd<T>(ContainerIdentifier containerId, EntryRelations entryRelations, EntryRelations entryRelationToMatch)
            where T : NonCompositeComponent
        {
            if (entryRelations.HasFlag(entryRelationToMatch))
            {
                var component = await _storage.Components.Retrieve<T>(containerId).ConfigureAwait(false);
                if (component != null)
                {
                    yield return component;
                }
            }
        }
    }
}
