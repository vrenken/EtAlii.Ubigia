namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api;

    public class EntrySet : IEntrySet
    {
        private readonly IEntryGetter _entryGetter;
        private readonly IEntryUpdater _entryUpdater;
        private readonly IEntryStorer _entryStorer;

        public EntrySet(
            IEntryGetter entryGetter, 
            IEntryUpdater entryUpdater, 
            IEntryStorer entryStorer)
        {
            _entryGetter = entryGetter;
            _entryUpdater = entryUpdater;
            _entryStorer = entryStorer;
        }

        public Entry Get(Identifier identifier, EntryRelation entryRelations)
        {
            return _entryGetter.Get(identifier, entryRelations);
        }

        public IEnumerable<Entry> GetRelated(Identifier identifier, EntryRelation entriesWithRelation, EntryRelation entryRelations)
        {
            return _entryGetter.GetRelated(identifier, entriesWithRelation, entryRelations);
        }

        public IEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelation entryRelations)
        {
            return _entryGetter.Get(identifiers, entryRelations);
        }

        public Entry Store(IEditableEntry entry)
        {
            return _entryStorer.Store(entry);
        }

        public Entry Store(Entry entry)
        {
            return _entryStorer.Store(entry);
        }

        public Entry Store(IEditableEntry entry, out IEnumerable<IComponent> storedComponents)
        {
            return _entryStorer.Store(entry, out storedComponents);
        }

        public Entry Store(Entry entry, out IEnumerable<IComponent> storedComponents)
        {
            return _entryStorer.Store(entry, out storedComponents);
        }

        public void Update(Entry entry, IEnumerable<IComponent> changedComponents)
        {
            _entryUpdater.Update(entry, changedComponents);
        }

        public void Update(IEditableEntry entry, IEnumerable<IComponent> changedComponents)
        {
            _entryUpdater.Update(entry, changedComponents);
        }
    }
}