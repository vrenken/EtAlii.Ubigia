namespace EtAlii.Servus.Api.Fabric
{
    using System.Threading.Tasks;

    internal class PropertyCacheRetrieveHandler : IPropertyCacheRetrieveHandler
    {
        private readonly IPropertyCacheHelper _cacheHelper;
        private readonly IPropertyCacheContextProvider _contextProvider;

        public PropertyCacheRetrieveHandler(
            IPropertyCacheHelper cacheHelper,
            IPropertyCacheContextProvider contextProvider)
        {
            _cacheHelper = cacheHelper;
            _contextProvider = contextProvider;
        }

        public async Task<PropertyDictionary> Handle(Identifier identifier, ExecutionScope scope)
        {
            var properties = _cacheHelper.GetProperties(identifier);
            if (properties == null)
            {
                properties = await _contextProvider.Context.Retrieve(identifier, scope);
                _cacheHelper.StoreProperties(identifier, properties);
            }
            return properties;
        }
    }
}
