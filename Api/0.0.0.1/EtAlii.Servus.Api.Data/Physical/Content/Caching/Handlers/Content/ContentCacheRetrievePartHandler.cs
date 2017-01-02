namespace EtAlii.Servus.Api.Data
{
    using System.Linq;
    using EtAlii.Servus.Api;
    using System;
    using System.Collections.Generic;

    internal class ContentCacheRetrievePartHandler
    {
        private readonly ContentCacheHelper _cacheHelper;
        private readonly ContentCacheContextProvider _contextProvider;

        public ContentCacheRetrievePartHandler(
            ContentCacheHelper cacheHelper,
            ContentCacheContextProvider contextProvider)
        {
            _cacheHelper = cacheHelper;
            _contextProvider = contextProvider;
        }

        public IReadOnlyContentPart Handle(Identifier identifier, UInt64 contentPartId)
        {
            var contentPart = _cacheHelper.Get(identifier, contentPartId);
            if (contentPart == null)
            {
                contentPart = _contextProvider.Context.Retrieve(identifier, contentPartId);
                if (contentPart != null)
                {
                    _cacheHelper.Store(identifier, contentPart);
                }
            }
            return contentPart;
        }
    }
}
