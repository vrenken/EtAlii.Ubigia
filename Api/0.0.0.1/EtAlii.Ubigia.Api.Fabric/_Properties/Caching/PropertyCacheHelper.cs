namespace EtAlii.Ubigia.Api.Fabric
{
    internal class PropertyCacheHelper : IPropertyCacheHelper
    {
        private readonly IPropertyCacheProvider _cacheProvider;

        public PropertyCacheHelper(IPropertyCacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public PropertyDictionary GetProperties(Identifier identifier)
        {
            PropertyDictionary properties = null;
            _cacheProvider.Cache.TryGetValue(identifier, out properties);
            return properties;
        }

        public void StoreProperties(Identifier identifier, PropertyDictionary properties)
        {
            _cacheProvider.Cache[identifier] = properties;
        }

        private void Invalidate(Identifier identifier)
        {
            PropertyDictionary properties;
            if (_cacheProvider.Cache.TryGetValue(identifier, out properties))
            {
                // Yup, we got a cache hit.
                _cacheProvider.Cache.Remove(identifier);
            }
        }
    }
}
