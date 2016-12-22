﻿namespace EtAlii.Servus.Api.Fabric
{
    using System.Collections.Generic;

    internal class PropertyCacheProvider : IPropertyCacheProvider
    {
        public IDictionary<Identifier, PropertyDictionary> Cache { get { return _cache; } }
        private readonly IDictionary<Identifier, PropertyDictionary> _cache;

        public PropertyCacheProvider()
        {
            _cache = new Dictionary<Identifier, PropertyDictionary>(100);
        }
    }
}
