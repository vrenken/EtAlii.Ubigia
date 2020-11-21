﻿namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Threading.Tasks;

    internal class ContentCacheStorePartHandler : IContentCacheStorePartHandler
    {
        private readonly IContentCacheHelper _cacheHelper;
        private readonly IContentCacheContextProvider _contextProvider;

        public ContentCacheStorePartHandler(
            IContentCacheHelper cacheHelper,
            IContentCacheContextProvider contextProvider)
        {
            _cacheHelper = cacheHelper;
            _contextProvider = contextProvider;
        }


        public async Task Handle(Identifier identifier, ContentPart contentPart)
        {
            await _contextProvider.Context.Store(identifier, contentPart).ConfigureAwait(false);
            _cacheHelper.Store(identifier, contentPart);
        }
    }
}
