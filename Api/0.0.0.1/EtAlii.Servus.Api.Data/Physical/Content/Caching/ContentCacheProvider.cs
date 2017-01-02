namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class ContentCacheProvider
    {
        public readonly IDictionary<Identifier, ContentCacheEntry> Cache;

        public ContentCacheProvider()
        {
            Cache = new Dictionary<Identifier, ContentCacheEntry>(100);
        }
    }
}
