namespace EtAlii.Servus.Api.Data
{
    using System.Linq;
    using EtAlii.Servus.Api;
    using System;
    using System.Collections.Generic;

    internal class ContentCacheRetrieveDefinitionHandler
    {
        private readonly ContentDefinitionCacheHelper _cacheHelper;
        private readonly ContentCacheContextProvider _contextProvider;

        public ContentCacheRetrieveDefinitionHandler(
            ContentDefinitionCacheHelper cacheHelper,
            ContentCacheContextProvider contextProvider)
        {
            _cacheHelper = cacheHelper;
            _contextProvider = contextProvider;
        }

        public IReadOnlyContentDefinition Handle(Identifier identifier)
        {
            var definition = _cacheHelper.Get(identifier);
            if (definition == null)
            {
                definition = _contextProvider.Context.RetrieveDefinition(identifier);
                if (definition != null && definition.Summary != null && definition.Summary.IsComplete)
                {
                    _cacheHelper.Store(identifier, definition);
                }
            }
            return definition;
        }
    }
}
