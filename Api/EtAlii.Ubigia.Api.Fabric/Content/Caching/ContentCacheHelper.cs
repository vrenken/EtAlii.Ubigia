namespace EtAlii.Ubigia.Api.Fabric
{
    internal class ContentCacheHelper : IContentCacheHelper
    {
        private readonly IContentCacheProvider _cacheProvider;

        public ContentCacheHelper(IContentCacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }
        

        public IReadOnlyContent Get(Identifier identifier)
        {
            _cacheProvider.Cache.TryGetValue(identifier, out var cacheEntry);
            return cacheEntry?.Content;
        }

        public IReadOnlyContentPart Get(Identifier identifier, ulong contentPartId)
        {
            var contentPart = default(IReadOnlyContentPart);

            if(_cacheProvider.Cache.TryGetValue(identifier, out var cacheEntry))
            {
                cacheEntry.ContentParts.TryGetValue(contentPartId, out contentPart);
            }
            return contentPart;
        }

        public void Store(Identifier identifier, IReadOnlyContent content)
        {
            if (!_cacheProvider.Cache.TryGetValue(identifier, out var cacheEntry))
            {
                cacheEntry = new ContentCacheEntry();
                _cacheProvider.Cache[identifier] = cacheEntry;
            }
            cacheEntry.Content = content;
        }

        public void Store(Identifier identifier, IReadOnlyContentPart contentPart)
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
