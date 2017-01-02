namespace EtAlii.Servus.Api.Data
{
    using System.Linq;
    using EtAlii.Servus.Api;
    using System;
    using System.Collections.Generic;

    internal class ContentCacheStoreDefinitionHandler
    {
        private readonly ContentDefinitionCacheHelper _cacheHelper;
        private readonly ContentCacheContextProvider _contextProvider;

        public ContentCacheStoreDefinitionHandler(
            ContentDefinitionCacheHelper cacheHelper,
            ContentCacheContextProvider contextProvider)
        {
            _cacheHelper = cacheHelper;
            _contextProvider = contextProvider;
        }

        public void Handle(Identifier identifier, ContentDefinition definition)
        {
            _contextProvider.Context.StoreDefinition(identifier, definition);

            if (definition.Summary != null && definition.Summary.IsComplete)
            {
                _cacheHelper.Store(identifier, definition);
            }
        }
    }
}
