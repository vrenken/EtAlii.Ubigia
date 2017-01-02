namespace EtAlii.Servus.Api.Fabric
{
    using System.Threading.Tasks;

    internal class PropertyCacheStoreHandler : IPropertyCacheStoreHandler
    {
        private readonly IPropertyCacheProvider _cacheProvider;
        private readonly IPropertyCacheContextProvider _contextProvider;

        public PropertyCacheStoreHandler(
            IPropertyCacheProvider cacheProvider, 
            IPropertyCacheContextProvider contextProvider)
        {
            _cacheProvider = cacheProvider;
            _contextProvider = contextProvider;
        }

        public async Task Handle(Identifier identifier)
        {
            await Task.Run(() =>
            {
                PropertyDictionary properties;
                if (_cacheProvider.Cache.TryGetValue(identifier, out properties))
                {
                    // Yup, we got a cache hit.
                    _cacheProvider.Cache.Remove(identifier);
                }
            });
        }

        public async Task Handle(Identifier identifier, PropertyDictionary properties, ExecutionScope scope)
        {
            await _contextProvider.Context.Store(identifier, properties, scope);
        }
    }
}
