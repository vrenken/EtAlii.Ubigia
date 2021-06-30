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

        private readonly IDictionary<Identifier, IReadOnlyEntry> _cache;
        private readonly IDictionary<Tuple<Identifier, EntryRelations>, IEnumerable<IReadOnlyEntry>> _cacheRelated;

        private readonly bool _cachingEnabled;

        public TraversalContextEntrySet(IFabricContext context)
        {
            _context = context;
            if (context.Configuration.TraversalCachingEnabled)
            {
                _cachingEnabled = true;
                _cache = new Dictionary<Identifier, IReadOnlyEntry>();
                _cacheRelated = new Dictionary<Tuple<Identifier, EntryRelations>, IEnumerable<IReadOnlyEntry>>();
            }
        }

        public async Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope)
        {
            IReadOnlyEntry result;

            if (_cachingEnabled)
            {
                if (!_cache.TryGetValue(entryIdentifier, out result))
                {
                    _cache[entryIdentifier] = result = await _context.Entries.Get(entryIdentifier, scope).ConfigureAwait(false);
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
                    if (!_cache.TryGetValue(entryIdentifier, out match))
                    {
                        _cache[entryIdentifier] = match = await _context.Entries.Get(entryIdentifier, scope).ConfigureAwait(false);
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
                if (!_cacheRelated.TryGetValue(key, out var result))
                {
                    _cacheRelated[key] = result = await _context.Entries
                        .GetRelated(identifier, relation, scope)
                        .ToArrayAsync()
                        .ConfigureAwait(false);
                }

                foreach (var entry in result)
                {
                    _cache[entry.Id] = entry;
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
