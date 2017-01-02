﻿namespace EtAlii.Servus.Api.Fabric
{
    using System.Threading.Tasks;

    internal class ContentCacheStoreHandler : IContentCacheStoreHandler
    {
        private readonly IContentCacheHelper _cacheHelper;
        private readonly IContentCacheContextProvider _contextProvider;

        public ContentCacheStoreHandler(
            IContentCacheHelper cacheHelper,
            IContentCacheContextProvider contextProvider)
        {
            _cacheHelper = cacheHelper;
            _contextProvider = contextProvider;
        }


        public async Task Handle(Identifier identifier, Content content)
        {
            await _contextProvider.Context.Store(identifier, content);

            if (content.Summary != null && content.Summary.IsComplete)
            {
                _cacheHelper.Store(identifier, content);
            }
        }
    }
}
