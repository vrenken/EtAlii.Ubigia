﻿namespace EtAlii.Servus.Api.Fabric
{
    using System.Collections.Generic;
    using System.Linq;

    internal class EntryCacheHelper : IEntryCacheHelper
    {
        private readonly IEntryCacheProvider _cacheProvider;

        public EntryCacheHelper(IEntryCacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public IReadOnlyEntry Get(Identifier identifier)
        {
            IReadOnlyEntry entry = null;
            _cacheProvider.Cache.TryGetValue(identifier, out entry);
            return entry;
        }

        public bool ShouldStore(IReadOnlyEntry entry)
        {
            return entry.Updates.Any();
        }

        public void Store(IReadOnlyEntry entry)
        {
            _cacheProvider.Cache[entry.Id] = entry;
        }

        public void InvalidateRelated(IReadOnlyEntry entry)
        {
            // We also want the following (forward oriented) items to be removed.
            Invalidate(entry.Children);
            Invalidate(entry.Downdate);
            Invalidate(entry.Indexed);
            Invalidate(entry.Indexes);
            Invalidate(entry.Parent);
            Invalidate(entry.Previous);
            Invalidate(entry.Updates);
        }

        /// <summary>
        /// Use this method to invalidate a set of relations all at once.
        /// </summary>
        /// <param name="relations"></param>
        /// <param name="cache"></param>
        private void Invalidate(IEnumerable<Relation> relations)
        {
            foreach (var relation in relations)
            {
                Invalidate(relation.Id);
            }
        }

        /// <summary>
        /// Invalidate a relation.
        /// </summary>
        /// <param name="relation"></param>
        /// <param name="cache"></param>
        private void Invalidate(Relation relation)
        {
            if (relation != Relation.None)
            {
                Invalidate(relation.Id);
            }
        }

        private void Invalidate(Identifier identifier)
        {
            IReadOnlyEntry entry;
            if (_cacheProvider.Cache.TryGetValue(identifier, out entry))
            {
                // Yup, we got a cache hit.
                _cacheProvider.Cache.Remove(entry.Id);
                //InvalidateRelated(entry, cache);
            }
        }
    }
}
