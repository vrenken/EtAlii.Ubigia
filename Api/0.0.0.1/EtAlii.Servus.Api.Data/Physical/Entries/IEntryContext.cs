namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System;
    using System.Collections.Generic;

    public interface IEntryContext
    {
        IEditableEntry Prepare();
        IReadOnlyEntry Change(IEditableEntry entry);
        IReadOnlyEntry Get(Root root);
        IReadOnlyEntry Get(Identifier entryIdentifier);
        IEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> entryIdentifiers);
        IEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation);

        event Action<Identifier> Prepared;
        event Action<Identifier> Stored;
    }
}
