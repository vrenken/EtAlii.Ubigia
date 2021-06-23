// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Threading.Tasks;

    internal class ContentCacheRetrieveDefinitionHandler : IContentCacheRetrieveDefinitionHandler
    {
        private readonly IContentDefinitionCacheHelper _cacheHelper;
        private readonly IContentCacheContextProvider _contextProvider;

        public ContentCacheRetrieveDefinitionHandler(
            IContentDefinitionCacheHelper cacheHelper,
            IContentCacheContextProvider contextProvider)
        {
            _cacheHelper = cacheHelper;
            _contextProvider = contextProvider;
        }

        public async Task<ContentDefinition> Handle(Identifier identifier)
        {
            var definition = _cacheHelper.Get(identifier);
            if (definition == null)
            {
                definition = await _contextProvider.Context.RetrieveDefinition(identifier).ConfigureAwait(false);
                if (definition?.Summary != null && definition.Summary.IsComplete)
                {
                    _cacheHelper.Store(identifier, definition);
                }
            }
            return definition;
        }
    }
}
