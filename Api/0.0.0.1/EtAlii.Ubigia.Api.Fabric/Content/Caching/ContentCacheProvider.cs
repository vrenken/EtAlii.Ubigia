namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Collections.Generic;

    internal class ContentCacheProvider : IContentCacheProvider
    {
        public IDictionary<Identifier, ContentCacheEntry> Cache { get { return _cache; } }
        private readonly IDictionary<Identifier, ContentCacheEntry> _cache;

        public ContentCacheProvider()
        {
            _cache = new Dictionary<Identifier, ContentCacheEntry>(100);
        }
    }
}
