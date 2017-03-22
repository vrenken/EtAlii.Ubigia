namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.MicroContainer;

    internal class PropertyContextScaffolding : IScaffolding
    {
        private readonly bool _enableCaching;

        public PropertyContextScaffolding(bool enableCaching)
        {
            _enableCaching = enableCaching;
        }

        public void Register(Container container)
        {
            if (_enableCaching)
            {
                //// Caching enabled.
                container.Register<IPropertyCacheRetrieveHandler, PropertyCacheRetrieveHandler>();
                container.Register<IPropertyCacheStoreHandler, PropertyCacheStoreHandler>();

                container.Register<IPropertyCacheProvider, PropertyCacheProvider>();
                container.Register<IPropertyCacheHelper, PropertyCacheHelper>();
                container.Register(() => CreatePropertyCacheContextProvider(container));
                container.Register<IPropertyContext, CachingPropertyContext>();
            }
            else
            {
                // Caching disabled.
                container.Register<IPropertyContext, PropertyContext>();
            }
        }

        private IPropertyCacheContextProvider CreatePropertyCacheContextProvider(Container container)
        {
            var connection = container.GetInstance<IDataConnection>();
            var context = new PropertyContext(connection); // we need to instantiate the original ContentContext and use it in the PropertyCacheContextProvider.
            return new PropertyCacheContextProvider(context);
        }
    }
}