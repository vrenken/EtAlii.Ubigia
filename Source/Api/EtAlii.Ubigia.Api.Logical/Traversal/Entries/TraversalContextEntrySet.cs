// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    internal sealed class TraversalContextEntrySet : ITraversalContextEntrySet
    {
        private readonly IFabricContext _context;

        public TraversalContextEntrySet(IFabricContext context)
        {
            _context = context;
        }

        public Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope)
        {
            return _context.Entries
                .Get(entryIdentifier, scope);
        }

        public IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> identifiers, ExecutionScope scope)
        {
            return _context.Entries
                .Get(identifiers, scope);
        }

        public IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier identifier, EntryRelations relation, ExecutionScope scope)
        {
            return _context.Entries
                .GetRelated(identifier, relation, scope);
        }
    }
}
