namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System.Collections.Generic;

    public interface IEntryDataClient : IDataClient
    {
        IEditableEntry Prepare();
        IReadOnlyEntry Change(IEditableEntry entry);
        IReadOnlyEntry Get(Root root, EntryRelation entryRelations = EntryRelation.None);
        IReadOnlyEntry Get(Identifier entryIdentifier, EntryRelation entryRelations = EntryRelation.None);
        IEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> entryIdentifiers, EntryRelation entryRelations = EntryRelation.None);

        IEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation, EntryRelation entryRelations = EntryRelation.None);
    }
}
