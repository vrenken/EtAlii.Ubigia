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

        public async IAsyncEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelation entryRelations)
        {
            foreach (var identifier in identifiers)
            {
                yield return await Get(identifier, entryRelations);
            }
        }

        public async IAsyncEnumerable<Entry> GetRelated(Identifier identifier, EntryRelation entriesWithRelation, EntryRelation entryRelations)
        {
            var entry = await Get(identifier, entriesWithRelation);

            var entries = AddHierarchicalEntries(entriesWithRelation, entryRelations, entry);
            await foreach (var e in entries)
            {
                yield return e;
            }

            entries = AddIndexEntries(entriesWithRelation, entryRelations, entry);
            await foreach (var e in entries)
            {
                yield return e;
            }

            entries = AddSequentialEntries(entriesWithRelation, entryRelations, entry);
            await foreach (var e in entries)
            {
                yield return e;
            }

            entries = AddTemporalEntries(entriesWithRelation, entryRelations, entry);
            await foreach (var e in entries)
            {
                yield return e;
            }
        }

        private async IAsyncEnumerable<Entry> AddTemporalEntries(
            EntryRelation entriesWithRelation, 
            EntryRelation entryRelations, 
            IReadOnlyEntry entry)
        {
            if (entriesWithRelation.HasFlag(EntryRelation.Downdate) && entry.Downdate != Relation.None)
            {
                yield return await Get(entry.Downdate.Id, entryRelations);
            }

            if (entriesWithRelation.HasFlag(EntryRelation.Update))
            {
                foreach (var update in entry.Updates)
                {
                    yield return await Get(update.Id, entryRelations);
                }
            }
        }

        private async IAsyncEnumerable<Entry> AddSequentialEntries(
            EntryRelation entriesWithRelation, 
            EntryRelation entryRelations, 
            IReadOnlyEntry entry)
        {
            if (entriesWithRelation.HasFlag(EntryRelation.Previous) && entry.Previous != Relation.None)
            {
                yield return await Get(entry.Previous.Id, entryRelations);
            }

            if (!entriesWithRelation.HasFlag(EntryRelation.Next) || entry.Next == Relation.None) yield break;
            yield return await Get(entry.Next.Id, entryRelations);
        }

        private async IAsyncEnumerable<Entry> AddIndexEntries(
            EntryRelation entriesWithRelation, 
            EntryRelation entryRelations,
            IReadOnlyEntry entry)
        {
            if (entriesWithRelation.HasFlag(EntryRelation.Index))
            {
                foreach (var index in entry.Indexes)
                {
                    yield return await Get(index.Id, entryRelations);
                }
            }

            if (!entriesWithRelation.HasFlag(EntryRelation.Indexed) || entry.Indexed == Relation.None) yield break;
            yield return await Get(entry.Parent.Id, entryRelations);
        }

        private async IAsyncEnumerable<Entry> AddHierarchicalEntries(
            EntryRelation entriesWithRelation, 
            EntryRelation entryRelations, 
            IReadOnlyEntry entry)
        {
            if (entriesWithRelation.HasFlag(EntryRelation.Parent))
            {
                if (entry.Parent != Relation.None)
                {
                    yield return await Get(entry.Parent.Id, entryRelations);
                }

                if (entry.Parent2 != Relation.None)
                {
                    yield return await Get(entry.Parent2.Id, entryRelations);
                }
            }

            if (!entriesWithRelation.HasFlag(EntryRelation.Child)) yield break;
            foreach (var child in entry.Children)
            {
                yield return await Get(child.Id, entryRelations);
            }

            foreach (var child2 in entry.Children2)
            {
                yield return await Get(child2.Id, entryRelations);
            }
        }

        public Task<Entry> Get(Identifier identifier, EntryRelation entryRelations)
        {
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);

            var selectedComponents = new List<IComponent>();

            RetrieveAndAdd<IdentifierComponent>(containerId, selectedComponents);
            RetrieveAndAdd<TypeComponent>(containerId, selectedComponents);
            RetrieveAndAdd<TagComponent>(containerId, selectedComponents);

            if (entryRelations.HasFlag(EntryRelation.Previous))
            {
                RetrieveAndAdd<PreviousComponent>(containerId, selectedComponents);
            }
            if (entryRelations.HasFlag(EntryRelation.Next))
            {
                RetrieveAndAdd<NextComponent>(containerId, selectedComponents);
            }
            if (entryRelations.HasFlag(EntryRelation.Update))
            {
                RetrieveAndAddAll<UpdatesComponent>(containerId, selectedComponents);
            }
            if (entryRelations.HasFlag(EntryRelation.Downdate))
            {
                RetrieveAndAdd<DowndateComponent>(containerId, selectedComponents);
            }
            if (entryRelations.HasFlag(EntryRelation.Parent))
            {
                RetrieveAndAdd<ParentComponent>(containerId, selectedComponents);
                RetrieveAndAdd<Parent2Component>(containerId, selectedComponents);
            }
            if (entryRelations.HasFlag(EntryRelation.Child))
            {
                RetrieveAndAddAll<ChildrenComponent>(containerId, selectedComponents);
                RetrieveAndAddAll<Children2Component>(containerId, selectedComponents);
            }
            if (entryRelations.HasFlag(EntryRelation.Index))
            {
                RetrieveAndAddAll<IndexesComponent>(containerId, selectedComponents);
            }
            if (entryRelations.HasFlag(EntryRelation.Indexed))
            {
                RetrieveAndAdd<IndexedComponent>(containerId, selectedComponents);
            }

            var entry = EntryHelper.Compose(selectedComponents, true);
            return Task.FromResult(entry);
        }

        private void RetrieveAndAddAll<T>(ContainerIdentifier containerId, List<IComponent> componentsToAddTo)
            where T : CompositeComponent
        {
            var components = _storage.Components.RetrieveAll<T>(containerId);
            if (components != null)
            {
                componentsToAddTo.AddRange(components);
            }
        }

        private void RetrieveAndAdd<T>(ContainerIdentifier containerId, List<IComponent> componentsToAddTo)
            where T : NonCompositeComponent
        {
            var component = _storage.Components.Retrieve<T>(containerId);
            if (component != null)
            {
                componentsToAddTo.Add(component);
            }
        }
    }
}