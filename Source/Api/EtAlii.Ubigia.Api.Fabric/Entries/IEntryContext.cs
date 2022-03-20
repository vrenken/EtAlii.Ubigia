// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEntryContext
    {
        Task<IEditableEntry> Prepare();
        Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope);
        Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope);
        Task<IReadOnlyEntry> Get(Identifier identifier, ExecutionScope scope);
        IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> identifiers, ExecutionScope scope);
        IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier identifier, EntryRelations relations, ExecutionScope scope);
    }
}
