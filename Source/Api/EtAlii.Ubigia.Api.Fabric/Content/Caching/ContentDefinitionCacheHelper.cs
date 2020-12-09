﻿namespace EtAlii.Ubigia.Api.Fabric
{
    internal class ContentDefinitionCacheHelper : IContentDefinitionCacheHelper
    {
        private readonly IContentCacheProvider _cacheProvider;

        public ContentDefinitionCacheHelper(IContentCacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public ContentDefinition Get(in Identifier identifier)
        {
            _cacheProvider.Cache.TryGetValue(identifier, out var cacheEntry);
            return cacheEntry != null ? cacheEntry.ContentDefinition : null;
        }

        public void Store(in Identifier identifier, ContentDefinition definition)
        {
            if (!_cacheProvider.Cache.TryGetValue(identifier, out var cacheEntry))
            {
                cacheEntry = new ContentCacheEntry();
                _cacheProvider.Cache[identifier] = cacheEntry;
            }
            cacheEntry.ContentDefinition = definition;
        }
    }
}
