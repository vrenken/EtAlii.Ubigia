namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Threading.Tasks;

    internal class PropertyCacheRetrieveHandler : IPropertyCacheRetrieveHandler
    {
        private readonly IPropertyCacheHelper _cacheHelper;
        private readonly IPropertiesCacheContextProvider _contextProvider;

        public PropertyCacheRetrieveHandler(
            IPropertyCacheHelper cacheHelper,
            IPropertiesCacheContextProvider contextProvider)
        {
            _cacheHelper = cacheHelper;
            _contextProvider = contextProvider;
        }

        public async Task<PropertyDictionary> Handle(Identifier identifier, ExecutionScope scope)
        {
            var properties = _cacheHelper.GetProperties(identifier);
            if (properties == null)
            {
                properties = await _contextProvider.Context.Retrieve(identifier, scope).ConfigureAwait(false);
                _cacheHelper.StoreProperties(identifier, properties);
            }
            return properties;
        }
    }
}
