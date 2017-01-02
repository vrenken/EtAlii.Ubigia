namespace EtAlii.Servus.Api.Data
{
    using System.Linq;
    using EtAlii.Servus.Api;
    using System;
    using System.Collections.Generic;

    internal class ContentCacheStoreHandler
    {
        private readonly ContentCacheHelper _cacheHelper;
        private readonly ContentCacheContextProvider _contextProvider;

        public ContentCacheStoreHandler(
            ContentCacheHelper cacheHelper,
            ContentCacheContextProvider contextProvider)
        {
            _cacheHelper = cacheHelper;
            _contextProvider = contextProvider;
        }


        public void Handle(Identifier identifier, Content content)
        {
            _contextProvider.Context.Store(identifier, content);

            if (content.Summary != null && content.Summary.IsComplete)
            {
                _cacheHelper.Store(identifier, content);
            }
        }
    }
}
