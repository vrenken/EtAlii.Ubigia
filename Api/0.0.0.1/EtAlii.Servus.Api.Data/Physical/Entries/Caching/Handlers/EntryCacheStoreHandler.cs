namespace EtAlii.Servus.Api.Data
{
    using System.Linq;
    using EtAlii.Servus.Api;
    using System;
    using System.Collections.Generic;

    public class EntryCacheStoreHandler
    {
        private readonly EntryCacheHelper _cacheHelper;
        private readonly EntryCacheProvider _cacheProvider;

        public EntryCacheStoreHandler(
            EntryCacheHelper cacheHelper, 
            EntryCacheProvider cacheProvider)
        {
            _cacheHelper = cacheHelper;
            _cacheProvider = cacheProvider;
        }

        public void Handle(Identifier identifier)
        {
            IReadOnlyEntry entry;
            if (_cacheProvider.Cache.TryGetValue(identifier, out entry))
            {
                // Yup, we got a cache hit.
                _cacheProvider.Cache.Remove(identifier);

                _cacheHelper.InvalidateRelated(entry);
            }
        }
    }
}
