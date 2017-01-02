namespace EtAlii.Servus.Api.Fabric
{
    using System;

    internal class ContentCacheHelper : IContentCacheHelper
    {
        private readonly IContentCacheProvider _cacheProvider;

        public ContentCacheHelper(IContentCacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }
        

        public IReadOnlyContent Get(Identifier identifier)
        {
            ContentCacheEntry cacheEntry = null;
            _cacheProvider.Cache.TryGetValue(identifier, out cacheEntry);
            return cacheEntry != null ? cacheEntry.Content : null;
        }

        public IReadOnlyContentPart Get(Identifier identifier, UInt64 contentPartId)
        {
            var contentPart = default(IReadOnlyContentPart);

            ContentCacheEntry cacheEntry = null;
            if(_cacheProvider.Cache.TryGetValue(identifier, out cacheEntry))
            {
                cacheEntry.ContentParts.TryGetValue(contentPartId, out contentPart);
            }
            return contentPart;
        }

        public void Store(Identifier identifier, IReadOnlyContent content)
        {
            ContentCacheEntry cacheEntry = null;
            if (!_cacheProvider.Cache.TryGetValue(identifier, out cacheEntry))
            {
                cacheEntry = new ContentCacheEntry();
                _cacheProvider.Cache[identifier] = cacheEntry;
            }
            cacheEntry.Content = content;
        }

        public void Store(Identifier identifier, IReadOnlyContentPart contentPart)
        {
            ContentCacheEntry cacheEntry = null;
            if (!_cacheProvider.Cache.TryGetValue(identifier, out cacheEntry))
            {
                cacheEntry = new ContentCacheEntry();
                _cacheProvider.Cache[identifier] = cacheEntry;
            }
            cacheEntry.ContentParts[contentPart.Id] = contentPart;
        }
    }
}
