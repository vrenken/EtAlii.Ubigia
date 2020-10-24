namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class LogicalEntrySet : ILogicalEntrySet
    {
        private readonly IFabricContext _fabricContext;
        private readonly IEntryPreparer _entryPreparer;

        public LogicalEntrySet(
            IFabricContext fabricContext, 
            IEntryPreparer entryPreparer)
        {
            _fabricContext = fabricContext;
            _entryPreparer = entryPreparer;
        }

        public Entry Prepare(Guid spaceId)
        {
            return _entryPreparer.Prepare(spaceId);
        }

        public Entry Prepare(Guid spaceId, Identifier id)
        {
            return _entryPreparer.Prepare(spaceId, id);
        }

        public Entry Get(Identifier identifier, EntryRelation entryRelations)
        {
            return _fabricContext.Entries.Get(identifier, entryRelations);
        }

        public IEnumerable<Entry> GetRelated(Identifier identifier, EntryRelation entriesWithRelation, EntryRelation entryRelations)
        {
            return _fabricContext.Entries.GetRelated(identifier, entriesWithRelation, entryRelations);
        }

        public IEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelation entryRelations)
        {
            return _fabricContext.Entries.Get(identifiers, entryRelations);
        }

        public Entry Store(IEditableEntry entry)
        {
            return _fabricContext.Entries.Store(entry);
        }

        public Entry Store(Entry entry)
        {
            return _fabricContext.Entries.Store(entry);
        }

        public Entry Store(IEditableEntry entry, out IEnumerable<IComponent> storedComponents)
        {
            return _fabricContext.Entries.Store(entry, out storedComponents);
        }

        public Entry Store(Entry entry, out IEnumerable<IComponent> storedComponents)
        {
            return _fabricContext.Entries.Store(entry, out storedComponents);
        }

        public void Update(Entry entry, IEnumerable<IComponent> changedComponents)
        {
            _fabricContext.Entries.Update(entry, changedComponents);
        }

        public void Update(IEditableEntry entry, IEnumerable<IComponent> changedComponents)
        {
            _fabricContext.Entries.Update(entry, changedComponents);
        }
    }
}