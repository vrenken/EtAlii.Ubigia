namespace EtAlii.Servus.Api.Fabric
{
    using System.Collections.Generic;

    internal interface IContentCacheProvider
    {
        IDictionary<Identifier, ContentCacheEntry> Cache { get; }
    }
}