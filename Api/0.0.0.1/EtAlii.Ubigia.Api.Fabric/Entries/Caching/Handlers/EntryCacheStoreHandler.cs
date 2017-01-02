﻿namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Threading.Tasks;

    internal class EntryCacheStoreHandler : IEntryCacheStoreHandler
    {
        private readonly IEntryCacheHelper _cacheHelper;
        private readonly IEntryCacheProvider _cacheProvider;

        public EntryCacheStoreHandler(
            IEntryCacheHelper cacheHelper, 
            IEntryCacheProvider cacheProvider)
        {
            _cacheHelper = cacheHelper;
            _cacheProvider = cacheProvider;
        }

        public async Task Handle(Identifier identifier)
        {
            await Task.Run(() =>
            {
                IReadOnlyEntry entry;
                if (_cacheProvider.Cache.TryGetValue(identifier, out entry))
                {
                    // Yup, we got a cache hit.
                    _cacheProvider.Cache.Remove(identifier);

                    _cacheHelper.InvalidateRelated(entry);
                }
            });
        }
    }
}
