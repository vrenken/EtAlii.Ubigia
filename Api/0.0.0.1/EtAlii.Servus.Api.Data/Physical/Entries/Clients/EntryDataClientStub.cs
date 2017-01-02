namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System.Collections.Generic;

    public class EntryDataClientStub : IEntryDataClient 
    {
        public IEditableEntry Prepare()
        {
            return null;
        }

        public IReadOnlyEntry Change(IEditableEntry entry)
        {
            return null;
        }

        public IReadOnlyEntry Get(Root root, EntryRelation entryRelations = EntryRelation.None)
        {
            return null;
        }

        public IReadOnlyEntry Get(Identifier entryIdentifier, EntryRelation entryRelations = EntryRelation.None)
        {
            return null;
        }

        public IEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> entryIdentifiers, EntryRelation entryRelations = EntryRelation.None)
        {
            return new IReadOnlyEntry[] {};
        }

        public IEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation, EntryRelation entryRelations = EntryRelation.None)
        {
            return null;
        }

        public void Connect()
        {
        }

        public void Disconnect()
        {
        }
    }
}
