namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Collections.Generic;

    internal class PropertyCacheProvider : IPropertyCacheProvider
    {
        public IDictionary<Identifier, PropertyDictionary> Cache { get; }

        public PropertyCacheProvider()
        {
            Cache = new Dictionary<Identifier, PropertyDictionary>(100);
        }
    }
}
