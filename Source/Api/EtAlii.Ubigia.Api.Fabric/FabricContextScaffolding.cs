// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric;

using EtAlii.Ubigia.Api.Transport;
using EtAlii.xTechnology.MicroContainer;

internal class FabricContextScaffolding : IScaffolding
{
    private readonly FabricOptions _options;

    public FabricContextScaffolding(FabricOptions options)
    {
        _options = options;
    }

    public void Register(IRegisterOnlyContainer container)
    {
        // Context
        container.Register<IFabricContext>(serviceCollection =>
        {
            var entryContext = serviceCollection.GetInstance<IEntryContext>();
            var rootContext = serviceCollection.GetInstance<IRootContext>();
            var contentContext = serviceCollection.GetInstance<IContentContext>();
            var dataConnection = serviceCollection.GetInstance<IDataConnection>();
            var propertiesContext = serviceCollection.GetInstance<IPropertiesContext>();

            return new FabricContext(_options, entryContext, rootContext, contentContext, dataConnection, propertiesContext);
        });
        container.Register(() => _options.ConfigurationRoot);

        // We want to be able to instantiate parts of the DI hierarchy also without a connection.
        container.Register(() => _options.Connection ?? new DataConnectionStub());

        // Content
        if (_options.CachingEnabled)
        {
            // Caching enabled.
            container.Register<IContentCacheRetrieveDefinitionHandler, ContentCacheRetrieveDefinitionHandler>();
            container.Register<IContentCacheStoreDefinitionHandler, ContentCacheStoreDefinitionHandler>();

            container.Register<IContentCacheRetrieveHandler, ContentCacheRetrieveHandler>();
            container.Register<IContentCacheRetrievePartHandler, ContentCacheRetrievePartHandler>();
            container.Register<IContentCacheStoreHandler, ContentCacheStoreHandler>();
            container.Register<IContentCacheStorePartHandler, ContentCacheStorePartHandler>();

            container.Register<IContentCacheProvider, ContentCacheProvider>();
            container.Register<IContentCacheHelper, ContentCacheHelper>();
            container.Register<IContentDefinitionCacheHelper, ContentDefinitionCacheHelper>();
            container.Register(CreateContentCacheContextProvider);
            container.Register<IContentContext, CachingContentContext>();
        }
        else
        {
            // Caching disabled.
            container.Register<IContentContext, ContentContext>();
        }

        // Entries
        container.Register<IEntryContext, EntryContext>();

        if (_options.CachingEnabled)
        {
            // Caching enabled.
            container.RegisterDecorator<IEntryContext, CachingEntryContext>();
        }

        // Roots
        container.Register<IRootContext, RootContext>();

        // Properties
        if (_options.CachingEnabled)
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

    private IContentCacheContextProvider CreateContentCacheContextProvider(IServiceCollection services)
    {
        var connection = services.GetInstance<IDataConnection>();
        var context = new ContentContext(connection); // we need to instantiate the original ContentContext and use it in the ContentCacheContextProvider.
        return new ContentCacheContextProvider(context);
    }

    private IPropertiesCacheContextProvider CreatePropertyCacheContextProvider(IServiceCollection services)
    {
        var connection = services.GetInstance<IDataConnection>();
        var context = new PropertiesContext(connection); // we need to instantiate the original ContentContext and use it in the PropertyCacheContextProvider.
        return new PropertiesCacheContextProvider(context);
    }
}
