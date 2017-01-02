namespace EtAlii.Servus.Api.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Fabric;

    internal class TraversalContextEntrySet : ITraversalContextEntrySet
    {
        private readonly IFabricContext _context;

        private readonly IDictionary<Identifier, IReadOnlyEntry> _cache;
        private readonly IDictionary<Tuple<Identifier, EntryRelation>, IEnumerable<IReadOnlyEntry>> _cacheRelated;

        private readonly bool _cachingEnabled;

        public TraversalContextEntrySet(IFabricContext context)
        {
            _context = context;
            if (context.Configuration.TraversalCachingEnabled)
            {
                _cachingEnabled = true;
                _cache = new Dictionary<Identifier, IReadOnlyEntry>();
                _cacheRelated = new Dictionary<Tuple<Identifier, EntryRelation>, IEnumerable<IReadOnlyEntry>>();
            }
        }

        public async Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope)
        {
            IReadOnlyEntry result;

            if (_cachingEnabled)
            {
                if (!_cache.TryGetValue(entryIdentifier, out result))
                {
                    _cache[entryIdentifier] = result = await _context.Entries.Get(entryIdentifier, scope);
                }
            }
            else
            {
                result = await _context.Entries.Get(entryIdentifier, scope);
            }

            return result;
        }

        public async Task<IEnumerable<IReadOnlyEntry>> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope)
        {
            var result = new List<IReadOnlyEntry>();

            foreach (var entryIdentifier in entryIdentifiers)
            {
                IReadOnlyEntry match;

                if (_cachingEnabled)
                {
                    if (!_cache.TryGetValue(entryIdentifier, out match))
                    {
                        _cache[entryIdentifier] = match = await _context.Entries.Get(entryIdentifier, scope);
                    }
                }
                else
                {
                    match = await _context.Entries.Get(entryIdentifier, scope);
                }
                result.Add(match);
            }
            return result;
        }

        public async Task<IEnumerable<IReadOnlyEntry>> GetRelated(Identifier identifier, EntryRelation relation, ExecutionScope scope)
        {
            var key = new Tuple<Identifier, EntryRelation>(identifier, relation);
            IEnumerable<IReadOnlyEntry> result;

            if (_cachingEnabled)
            {
                if (!_cacheRelated.TryGetValue(key, out result))
                {
                    _cacheRelated[key] = result = await _context.Entries.GetRelated(identifier, relation, scope);
                }

                foreach (var entry in result)
                {
                    _cache[entry.Id] = entry;
                }
            }
            else
            {
                result = await _context.Entries.GetRelated(identifier, relation, scope);
            }
            return result;
        }
    }
}