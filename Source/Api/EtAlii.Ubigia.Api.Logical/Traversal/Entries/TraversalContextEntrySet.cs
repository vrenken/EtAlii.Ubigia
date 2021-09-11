// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    internal class TraversalContextEntrySet : ITraversalContextEntrySet
    {
        private readonly IFabricContext _context;

        private readonly bool _cachingEnabled;

        public TraversalContextEntrySet(IFabricContext context)
        {
            _context = context;
            //_cachingEnabled = _context.Options.CachingEnabled;
            _cachingEnabled = false;// TODO: CF42 Caching does not work yet.
        }

        public async Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope)
        {
            IReadOnlyEntry result;

            if (_cachingEnabled)
            {
                if (!scope.EntryCache.TryGetValue(entryIdentifier, out result))
                {
                    scope.EntryCache[entryIdentifier] = result = await _context.Entries.Get(entryIdentifier, scope).ConfigureAwait(false);
                }
            }
            else
            {
                result = await _context.Entries.Get(entryIdentifier, scope).ConfigureAwait(false);
            }

            return result;
        }

        public async IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> identifiers, ExecutionScope scope)
        {
            foreach (var entryIdentifier in identifiers)
            {
                IReadOnlyEntry match;

                if (_cachingEnabled)
                {
                    if (!scope.EntryCache.TryGetValue(entryIdentifier, out match))
                    {
                        scope.EntryCache[entryIdentifier] = match = await _context.Entries.Get(entryIdentifier, scope).ConfigureAwait(false);
                    }
                }
                else
                {
                    match = await _context.Entries.Get(entryIdentifier, scope).ConfigureAwait(false);
                }

                yield return match;
            }
        }

        public async IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier identifier, EntryRelations relation, ExecutionScope scope)
        {
            var key = new Tuple<Identifier, EntryRelations>(identifier, relation);

            if (_cachingEnabled)
            {
                if (!scope.EntryRelationCache.TryGetValue(key, out var result))
                {
                    scope.EntryRelationCache[key] = result = await _context.Entries
                        .GetRelated(identifier, relation, scope)
                        .ToArrayAsync()
                        .ConfigureAwait(false);
                }

                foreach (var entry in result)
                {
                    scope.EntryCache[entry.Id] = entry;
                    yield return entry;
                }
            }
            else
            {
                var result = _context.Entries
                    .GetRelated(identifier, relation, scope)
                    .ConfigureAwait(false);
                await foreach (var entry in result)
                {
                    yield return entry;
                }
            }
        }
    }
}
