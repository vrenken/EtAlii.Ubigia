// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric;

using EtAlii.xTechnology.MicroContainer;

internal class CommonFabricExtension : IExtension
{
    private readonly FabricOptions _options;

    public CommonFabricExtension(FabricOptions options)
    {
        _options = options;
    }

    public void Initialize(IRegisterOnlyContainer container)
    {
        new ContextScaffolding(_options).Register(container);
        new EntryContextScaffolding(_options.CachingEnabled).Register(container);
        new ContentContextScaffolding(_options.CachingEnabled).Register(container);
        new PropertyContextScaffolding(_options.CachingEnabled).Register(container);
        new RootContextScaffolding().Register(container);
    }
}
