// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILogicalEntrySet
    {
        Task<Entry> Prepare(Guid spaceId);
        Task<Entry> Prepare(Guid spaceId, Identifier id);

        Task<Entry> Get(Identifier identifier, EntryRelation entryRelations);
        IAsyncEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelation entryRelations);
        IAsyncEnumerable<Entry> GetRelated(Identifier identifier, EntryRelation entriesWithRelation, EntryRelation entryRelations);

        Entry Store(IEditableEntry entry);
        Entry Store(Entry entry);

        Entry Store(IEditableEntry entry, out IEnumerable<IComponent> storedComponents);
        Entry Store(Entry entry, out IEnumerable<IComponent> storedComponents);

        void Update(Entry entry, IEnumerable<IComponent> changedComponents);
        void Update(IEditableEntry entry, IEnumerable<IComponent> changedComponents);

    }
}