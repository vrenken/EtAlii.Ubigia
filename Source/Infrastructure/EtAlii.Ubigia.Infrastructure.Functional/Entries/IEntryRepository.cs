// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEntryRepository
    {
        Task<Entry> Get(Identifier identifier, EntryRelations entryRelations = EntryRelations.None);
        IAsyncEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelations entryRelations = EntryRelations.None);
        IAsyncEnumerable<Entry> GetRelated(Identifier identifier, EntryRelations entriesWithRelation, EntryRelations entryRelations = EntryRelations.None);

        Task<Entry> Prepare(Guid spaceId);
        Task<Entry> Prepare(Guid spaceId, Identifier identifier);

        Entry Store(Entry entry);
        Entry Store(IEditableEntry entry);
    }
}