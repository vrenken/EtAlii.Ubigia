// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITraversalContextEntrySet
    {
        Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope);
        IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> identifiers, ExecutionScope scope);
        IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier identifier, EntryRelations relation, ExecutionScope scope);
    }
}
