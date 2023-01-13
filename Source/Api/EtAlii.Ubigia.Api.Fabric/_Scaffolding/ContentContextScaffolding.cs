// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric;

using EtAlii.Ubigia.Api.Transport;
using EtAlii.xTechnology.MicroContainer;

internal class ContentContextScaffolding : IScaffolding
{
    private readonly bool _enableCaching;

    public ContentContextScaffolding(bool enableCaching)
    {
        _enableCaching = enableCaching;
    }

    public void Register(IRegisterOnlyContainer container)
    {
        if (_enableCaching)
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
    }

    private IContentCacheContextProvider CreateContentCacheContextProvider(IServiceCollection services)
    {
        var connection = services.GetInstance<IDataConnection>();
        var context = new ContentContext(connection); // we need to instantiate the original ContentContext and use it in the ContentCacheContextProvider.
        return new ContentCacheContextProvider(context);
    }

}
