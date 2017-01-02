namespace EtAlii.Servus.Api.Data
{
    using System.Linq;
    using EtAlii.Servus.Api;
    using System;
    using System.Collections.Generic;

    internal class ContentCacheRetrieveHandler
    {
        private readonly ContentCacheHelper _cacheHelper;
        private readonly ContentCacheContextProvider _contextProvider;

        public ContentCacheRetrieveHandler(
            ContentCacheHelper cacheHelper,
            ContentCacheContextProvider contextProvider)
        {
            _cacheHelper = cacheHelper;
            _contextProvider = contextProvider;
        }

        public IReadOnlyContent Handle(Identifier identifier)
        {
            var content = _cacheHelper.Get(identifier);
            if (content == null)
            {
                content = _contextProvider.Context.Retrieve(identifier);
                if (content != null && content.Summary.IsComplete)
                {
                    _cacheHelper.Store(identifier, content);
                }
            }
            return content;
        }
    }
}
