// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEntryGetter
    {
        IAsyncEnumerable<Entry> GetRelated(Identifier identifier, EntryRelation entriesWithRelation, EntryRelation entryRelations);
        Task<Entry> Get(Identifier identifier, EntryRelation entryRelations);
        IAsyncEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelation entryRelations);
    }
}