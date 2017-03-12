namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api;

    public interface IEntrySet
    {
        Entry Get(Identifier identifier, EntryRelation entryRelations);
        IEnumerable<Entry> GetRelated(Identifier identifier, EntryRelation entriesWithRelation, EntryRelation entryRelations);
        IEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelation entryRelations);

        Entry Store(IEditableEntry entry);
        Entry Store(Entry entry);

        Entry Store(IEditableEntry entry, out IEnumerable<IComponent> storedComponents);
        Entry Store(Entry entry, out IEnumerable<IComponent> storedComponents);

        void Update(Entry entry, IEnumerable<IComponent> changedComponents);
        void Update(IEditableEntry entry, IEnumerable<IComponent> changedComponents);

    }
}