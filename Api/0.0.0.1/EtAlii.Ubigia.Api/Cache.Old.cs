namespace EtAlii.Ubigia.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class OldCache
    {
        private readonly bool _cacheEnabled;
        private readonly IDictionary<Identifier, IReadOnlyEntry> _entries;

        private readonly IDictionary<Tuple<Identifier, EntryRelation>, IEnumerable<IReadOnlyEntry>> _relatedEntries;

        private readonly IDictionary<string, Root> _roots;

        private readonly IDictionary<Identifier, PropertyDictionary> _properties;

        public OldCache(bool cacheEnabled = true)
        {
            _cacheEnabled = cacheEnabled;
            _entries = new Dictionary<Identifier, IReadOnlyEntry>();
            _relatedEntries = new Dictionary<Tuple<Identifier, EntryRelation>, IEnumerable<IReadOnlyEntry>>();
            _roots = new Dictionary<string, Root>();
            _properties = new Dictionary<Identifier, PropertyDictionary>();
        }

        public async Task<PropertyDictionary> GetProperties(Identifier identifier, Func<Task<PropertyDictionary>> getter)
        {
            PropertyDictionary result;

            if (_cacheEnabled)
            {
                bool hasValue;
                //lock (_properties)
                {
                    hasValue = _properties.TryGetValue(identifier, out result);
                }
                if(!hasValue)
                {
                    //lock (_properties)
                    {
                        _properties[identifier] = result = await getter();
                    }
                }
            }
            else
            {
                result = await getter();
            }

            return result;
        }

        public async Task<IReadOnlyEntry> GetEntry(Identifier identifier, Func<Task<IReadOnlyEntry>> getter)
        {
            IReadOnlyEntry result;

            if (_cacheEnabled)
            {
                bool hasValue;
                //lock (_entries)
                {
                    // TODO: This cache is not clever enough yet.
                    hasValue = _entries.TryGetValue(identifier, out result);
                }
                if(!hasValue)
                {
                    //lock (_entries)
                    {
                        _entries[identifier] = result = await getter();
                    }
                }
            }
            else
            {
                result = await getter();
            }

            return result;
        }

        public async Task<IEnumerable<IReadOnlyEntry>> GetRelatedEntries(Identifier identifier, EntryRelation relation, Func<Task<IEnumerable<IReadOnlyEntry>>> getter)
        {
            IEnumerable<IReadOnlyEntry> result;

            if (_cacheEnabled)
            {
                var cacheId = new Tuple<Identifier, EntryRelation>(identifier, relation);

                bool hasValue;
                //lock (_relatedEntries)
                {
                    // TODO: This cache is not clever enough yet.
                    hasValue = _relatedEntries.TryGetValue(cacheId, out result);
                }
                if (!hasValue)
                {
                    //lock (_relatedEntries)
                    {
                        _relatedEntries[cacheId] = result = await getter();
                    }

                }
            }
            else
            {
                result = await getter();
            }

            return result;
        }

        public void InvalidateEntry(Identifier identifier)
        {
            //lock (_entries)
            {
                _entries.Remove(identifier);
            }

            //lock (_relatedEntries)
            {
                var itemsToRemove = new List<Tuple<Identifier, EntryRelation>>();

                var items = _relatedEntries
                    .Where(r => r.Key.Item1 == identifier)
                    .Select(r => r.Key)
                    .ToArray();
                itemsToRemove.AddRange(items);

                items = _relatedEntries
                    .Where(r => r.Value.Any(e => e.Id == identifier))
                    .Select(r => r.Key)
                    .ToArray();
                itemsToRemove.AddRange(items);

                foreach (var itemToRemove in itemsToRemove)
                {
                    _relatedEntries.Remove(itemToRemove);
                }
            }
        }
    }
}