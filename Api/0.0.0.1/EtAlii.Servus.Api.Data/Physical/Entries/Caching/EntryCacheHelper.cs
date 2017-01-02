namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System.Collections.Generic;

    public class EntryCacheHelper
    {
        private readonly EntryCacheProvider _cacheProvider;

        public EntryCacheHelper(EntryCacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public IReadOnlyEntry GetEntry(Identifier identifier)
        {
            IReadOnlyEntry entry = null;
            _cacheProvider.Cache.TryGetValue(identifier, out entry);
            return entry;
        }

        public void StoreEntry(IReadOnlyEntry entry)
        {
            // TODO: Or should we not always try to cache everything?
            //if (entry.Updates.Any())
            //{
            _cacheProvider.Cache[entry.Id] = entry;
            //}
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
