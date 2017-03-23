namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Collections.Generic;

    internal class ContentCacheProvider : IContentCacheProvider
    {
        public IDictionary<Identifier, ContentCacheEntry> Cache { get; }

        public ContentCacheProvider()
        {
            Cache = new Dictionary<Identifier, ContentCacheEntry>(100);
        }
    }
}
