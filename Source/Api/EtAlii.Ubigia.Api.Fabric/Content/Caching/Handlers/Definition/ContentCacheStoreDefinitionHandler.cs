// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Threading.Tasks;

    internal class ContentCacheStoreDefinitionHandler : IContentCacheStoreDefinitionHandler
    {
        private readonly IContentDefinitionCacheHelper _cacheHelper;
        private readonly IContentCacheContextProvider _contextProvider;

        public ContentCacheStoreDefinitionHandler(
            IContentDefinitionCacheHelper cacheHelper,
            IContentCacheContextProvider contextProvider)
        {
            _cacheHelper = cacheHelper;
            _contextProvider = contextProvider;
        }

        public async Task Handle(Identifier identifier, ContentDefinition definition)
        {
            await _contextProvider.Context.StoreDefinition(identifier, definition).ConfigureAwait(false);

            if (definition.Summary != null && definition.Summary.IsComplete)
            {
                _cacheHelper.Store(identifier, definition);
            }
        }
    }
}
