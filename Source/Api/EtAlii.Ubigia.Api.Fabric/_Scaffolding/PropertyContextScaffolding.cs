// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric;

using EtAlii.Ubigia.Api.Transport;
using EtAlii.xTechnology.MicroContainer;

internal class PropertyContextScaffolding : IScaffolding
{
    private readonly bool _enableCaching;

    public PropertyContextScaffolding(bool enableCaching)
    {
        _enableCaching = enableCaching;
    }

    public void Register(IRegisterOnlyContainer container)
    {
        if (_enableCaching)
        {
            //// Caching enabled.
            container.Register<IPropertyCacheRetrieveHandler, PropertyCacheRetrieveHandler>();
            container.Register<IPropertyCacheStoreHandler, PropertyCacheStoreHandler>();

            container.Register<IPropertyCacheProvider, PropertyCacheProvider>();
            container.Register<IPropertyCacheHelper, PropertyCacheHelper>();
            container.Register(CreatePropertyCacheContextProvider);
            container.Register<IPropertiesContext, CachingPropertiesContext>();
        }
        else
        {
            // Caching disabled.
            container.Register<IPropertiesContext, PropertiesContext>();
        }
    }

    private IPropertiesCacheContextProvider CreatePropertyCacheContextProvider(IServiceCollection services)
    {
        var connection = services.GetInstance<IDataConnection>();
        var context = new PropertiesContext(connection); // we need to instantiate the original ContentContext and use it in the PropertyCacheContextProvider.
        return new PropertiesCacheContextProvider(context);
    }
}
