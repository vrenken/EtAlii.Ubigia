namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api;

    public interface IEntryRepository
    {
        Entry Get(Identifier identifier, EntryRelation entryRelations = EntryRelation.None);
        IEnumerable<Entry> GetRelated(Identifier identifier, EntryRelation entriesWithRelation, EntryRelation entryRelations = EntryRelation.None);
        IEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelation entryRelations = EntryRelation.None);

        Entry Prepare(Guid spaceId);
        Entry Prepare(Guid spaceId, Identifier id);

        Entry Store(Entry entry);
        Entry Store(IEditableEntry entry);
    }
}