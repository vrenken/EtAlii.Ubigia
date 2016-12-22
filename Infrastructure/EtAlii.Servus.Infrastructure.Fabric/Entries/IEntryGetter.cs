namespace EtAlii.Servus.Infrastructure.Fabric
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api;

    public interface IEntryGetter
    {
        Entry Get(Identifier identifier, EntryRelation entryRelations);
        IEnumerable<Entry> GetRelated(Identifier identifier, EntryRelation entriesWithRelation, EntryRelation entryRelations);
        IEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelation entryRelations);
    }
}