namespace EtAlii.Servus.Api.Data
{
    using System.Linq;
    using EtAlii.Servus.Api;
    using System;
    using System.Collections.Generic;

    internal class ContentCacheStorePartHandler
    {
        private readonly ContentCacheHelper _cacheHelper;
        private readonly ContentCacheContextProvider _contextProvider;

        public ContentCacheStorePartHandler(
            ContentCacheHelper cacheHelper,
            ContentCacheContextProvider contextProvider)
        {
            _cacheHelper = cacheHelper;
            _contextProvider = contextProvider;
        }


        public void Handle(Identifier identifier, ContentPart contentPart)
        {
            _contextProvider.Context.Store(identifier, contentPart);
            _cacheHelper.Store(identifier, contentPart);
        }
    }
}
