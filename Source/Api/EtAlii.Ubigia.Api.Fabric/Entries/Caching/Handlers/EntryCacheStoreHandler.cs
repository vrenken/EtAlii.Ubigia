// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
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

        public Task Handle(Identifier identifier)
        {
            if (_cacheProvider.Cache.TryGetValue(identifier, out var entry))
            {
                // Yup, we got a cache hit.
                _cacheProvider.Cache.Remove(identifier);

                _cacheHelper.InvalidateRelated(entry);
            }

            return Task.CompletedTask;
        }
    }
}
