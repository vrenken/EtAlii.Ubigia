// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    internal class ContentCacheHelper : IContentCacheHelper
    {
        private readonly IContentCacheProvider _cacheProvider;

        public ContentCacheHelper(IContentCacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }


        public Content Get(in Identifier identifier)
        {
            _cacheProvider.Cache.TryGetValue(identifier, out var cacheEntry);
            return cacheEntry?.Content;
        }

        public ContentPart Get(in Identifier identifier, ulong contentPartId)
        {
            var contentPart = default(ContentPart);

            if(_cacheProvider.Cache.TryGetValue(identifier, out var cacheEntry))
            {
                cacheEntry.ContentParts.TryGetValue(contentPartId, out contentPart);
            }
            return contentPart;
        }

        public void Store(in Identifier identifier, Content content)
        {
            if (!_cacheProvider.Cache.TryGetValue(identifier, out var cacheEntry))
            {
                cacheEntry = new ContentCacheEntry();
                _cacheProvider.Cache[identifier] = cacheEntry;
            }
            cacheEntry.Content = content;
        }

        public void Store(in Identifier identifier, ContentPart contentPart)
        {
            if (!_cacheProvider.Cache.TryGetValue(identifier, out var cacheEntry))
            {
                cacheEntry = new ContentCacheEntry();
                _cacheProvider.Cache[identifier] = cacheEntry;
            }
            cacheEntry.ContentParts[contentPart.Id] = contentPart;
        }
    }
}
