﻿namespace EtAlii.Ubigia.Api.Fabric
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEntryContext
    {
        Task<IEditableEntry> Prepare();
        Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope);
        Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope);
        Task<IReadOnlyEntry> Get(Identifier identifier, ExecutionScope scope);
        Task<IEnumerable<IReadOnlyEntry>> Get(IEnumerable<Identifier> identifiers, ExecutionScope scope);
        IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier identifier, EntryRelation relations, ExecutionScope scope);

        event Action<Identifier> Prepared;
        event Action<Identifier> Stored;
    }
}
