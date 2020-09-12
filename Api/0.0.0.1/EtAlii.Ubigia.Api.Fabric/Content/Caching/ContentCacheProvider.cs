namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    internal class ContentCacheProvider : IContentCacheProvider
    {
        public IDictionary<Identifier, ContentCacheEntry> Cache { get; }

        public ContentCacheProvider()
        {
            Cache = new ConcurrentDictionary<Identifier, ContentCacheEntry>();
        }
    }
}
