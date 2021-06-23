// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    internal class PropertyCacheHelper : IPropertyCacheHelper
    {
        private readonly IPropertyCacheProvider _cacheProvider;

        public PropertyCacheHelper(IPropertyCacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public PropertyDictionary GetProperties(in Identifier identifier)
        {
            _cacheProvider.Cache.TryGetValue(identifier, out var properties);
            return properties;
        }

        public void StoreProperties(in Identifier identifier, PropertyDictionary properties)
        {
            _cacheProvider.Cache[identifier] = properties;
        }
    }
}
