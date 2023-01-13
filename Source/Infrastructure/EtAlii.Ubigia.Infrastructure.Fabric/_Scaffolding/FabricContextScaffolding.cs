// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using EtAlii.xTechnology.MicroContainer;

internal class FabricContextScaffolding : IScaffolding
{
    private readonly FabricContextOptions _options;

    public FabricContextScaffolding(FabricContextOptions options)
    {
        _options = options;
    }

    public void Register(IRegisterOnlyContainer container)
    {
        container.Register<IFabricContext, FabricContext>();
        container.Register(() => _options.Storage);
        container.Register(() => _options.ConfigurationRoot);
    }
}
