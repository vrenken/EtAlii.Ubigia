// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// This Cache implementation isn't that cool yet. It needs a serious revamp.
    /// </summary>
    public class Cache
    {
        private readonly SemaphoreSlim _propertiesSemaphore = new(1,1);
        private readonly SemaphoreSlim _entriesSemaphore = new(1,1);
        private readonly SemaphoreSlim _relatedEntriesSemaphore = new(1,1);
        private readonly bool _cacheEnabled;
        private readonly IDictionary<Identifier, IReadOnlyEntry> _entries;

        private readonly IDictionary<Tuple<Identifier, EntryRelations>, IEnumerable<IReadOnlyEntry>> _relatedEntries;

        //private readonly IDictionary<string, Root> _roots

        private readonly IDictionary<Identifier, PropertyDictionary> _properties;

        /// <summary>
        /// Create a new Cache instance. Set the cacheEnabled to false if the cache should be disabled.
        /// </summary>
        /// <param name="cacheEnabled">False when caching should be disabled.</param>
        public Cache(bool cacheEnabled = true)
        {
            _cacheEnabled = cacheEnabled;
            _entries = new Dictionary<Identifier, IReadOnlyEntry>();
            _relatedEntries = new Dictionary<Tuple<Identifier, EntryRelations>, IEnumerable<IReadOnlyEntry>>();
            //_roots = new Dictionary<string, Root>()
            _properties = new Dictionary<Identifier, PropertyDictionary>();
        }

        /// <summary>
        /// Returns the properties for the specified identifier.
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="getter"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<PropertyDictionary> GetProperties(Identifier identifier, Func<Task<PropertyDictionary>> getter)
        {
            PropertyDictionary result;

            if (_cacheEnabled)
            {
                await _propertiesSemaphore.WaitAsync().ConfigureAwait(false);

                var hasValue = _properties.TryGetValue(identifier, out result);

                _propertiesSemaphore.Release();

                if(!hasValue)
                {
                    await _propertiesSemaphore.WaitAsync().ConfigureAwait(false);

                    _properties[identifier] = result = await getter().ConfigureAwait(false);

                    _propertiesSemaphore.Release();
                }
            }
            else
            {
                result = await getter().ConfigureAwait(false);
            }

            return result;
        }

        /// <summary>
        /// Returns the entry for the specified identifier.
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="getter"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IReadOnlyEntry> GetEntry(Identifier identifier, Func<Task<IReadOnlyEntry>> getter)
        {
            IReadOnlyEntry result;

            if (_cacheEnabled)
            {
                await _entriesSemaphore.WaitAsync().ConfigureAwait(false);

                // TODO: This cache is not clever enough yet.
                var hasValue = _entries.TryGetValue(identifier, out result);

                _entriesSemaphore.Release();

                if(!hasValue)
                {
                    await _entriesSemaphore.WaitAsync().ConfigureAwait(false);
                    _entries[identifier] = result = await getter().ConfigureAwait(false);
                    _entriesSemaphore.Release();
                }
            }
            else
            {
                result = await getter().ConfigureAwait(false);
            }

            return result;
        }

        /// <summary>
        /// Returns the related entries for the specified identifier and relation.
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="relation"></param>
        /// <param name="getter"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async IAsyncEnumerable<IReadOnlyEntry> GetRelatedEntries(Identifier identifier, EntryRelations relation, Func<IAsyncEnumerable<IReadOnlyEntry>> getter)
        {
            if (_cacheEnabled)
            {
                var cacheId = new Tuple<Identifier, EntryRelations>(identifier, relation);

                await _relatedEntriesSemaphore.WaitAsync().ConfigureAwait(false);

                // TODO: This cache is not clever enough yet.
                var hasValue = _relatedEntries.TryGetValue(cacheId, out var cachedResult);

                _relatedEntriesSemaphore.Release();

                if (hasValue)
                {
                    foreach (var item in cachedResult)
                    {
                        yield return item;
                    }
                }
                else
                {
                    await _relatedEntriesSemaphore.WaitAsync().ConfigureAwait(false);

                    var list = new List<IReadOnlyEntry>();
                    var result = getter()
                        .ConfigureAwait(false);

                    await foreach (var item in result)
                    {
                        list.Add(item);
                        yield return item;
                    }

                    _relatedEntries[cacheId] = list;

                    _relatedEntriesSemaphore.Release();
                }
            }
            else
            {
                var result = getter().ConfigureAwait(false);
                await foreach (var item in result)
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Invalidates all cache entries for the specified identifier.
        /// </summary>
        /// <param name="identifier"></param>
        public void InvalidateEntry(Identifier identifier)
        {
            _entriesSemaphore.Wait();
            _relatedEntriesSemaphore.Wait();

            _entries.Remove(identifier);

            var itemsToRemove = new List<Tuple<Identifier, EntryRelations>>();

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

            _entriesSemaphore.Release();
            _relatedEntriesSemaphore.Release();
        }
    }
}
