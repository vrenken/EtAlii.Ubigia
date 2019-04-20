namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Storage;

    internal class EntryGetter : IEntryGetter
    {
        private readonly IStorage _storage;

        public EntryGetter(IStorage storage)
        {
            _storage = storage;
        }

        public IEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelation entryRelations)
        {
            return identifiers.Select(identifier => Get(identifier, entryRelations))
                              .AsEnumerable();
        }

        public IEnumerable<Entry> GetRelated(Identifier identifier, EntryRelation entriesWithRelation, EntryRelation entryRelations)
        {
            var entry = Get(identifier, entriesWithRelation);

            var entries = new List<Entry>();

            if (entriesWithRelation.HasFlag(EntryRelation.Parent))
            {
                if (entry.Parent != Relation.None)
                {
                    var parentEntry = Get(entry.Parent.Id, entryRelations);
                    entries.Add(parentEntry);
                }

                if (entry.Parent2 != Relation.None)
                {
                    var parent2Entry = Get(entry.Parent2.Id, entryRelations);
                    entries.Add(parent2Entry);
                }
            }

            if (entriesWithRelation.HasFlag(EntryRelation.Child))
            {
                entries.AddRange(entry.Children.Select(relation => Get(relation.Id, entryRelations)));
                entries.AddRange(entry.Children2.Select(relation => Get(relation.Id, entryRelations)));
            }

            if (entriesWithRelation.HasFlag(EntryRelation.Index))
            {
                entries.AddRange(entry.Indexes.Select(relation => Get(relation.Id, entryRelations)));
            }

            if (entriesWithRelation.HasFlag(EntryRelation.Indexed) && entry.Indexed != Relation.None)
            {
                var indexedEntry = Get(entry.Parent.Id, entryRelations);
                entries.Add(indexedEntry);
            }

            if (entriesWithRelation.HasFlag(EntryRelation.Previous) && entry.Previous != Relation.None)
            {
                var previousEntry = Get(entry.Previous.Id, entryRelations);
                entries.Add(previousEntry);
            }

            if (entriesWithRelation.HasFlag(EntryRelation.Next) && entry.Next != Relation.None)
            {
                var nextEntry = Get(entry.Next.Id, entryRelations);
                entries.Add(nextEntry);
            }

            if (entriesWithRelation.HasFlag(EntryRelation.Downdate) && entry.Downdate != Relation.None)
            {
                var downdateEntry = Get(entry.Downdate.Id, entryRelations);
                entries.Add(downdateEntry);
            }

            if (entriesWithRelation.HasFlag(EntryRelation.Update))
            {
                entries.AddRange(entry.Updates.Select(relation => Get(relation.Id, entryRelations)));
            }

            return entries;
        }

        public Entry Get(Identifier identifier, EntryRelation entryRelations)
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

            return EntryHelper.Compose(selectedComponents, true);
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