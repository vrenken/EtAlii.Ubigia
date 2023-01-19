// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric;

using EtAlii.xTechnology.MicroContainer;

internal class FabricContextExtension : IExtension
{
    private readonly FabricOptions _options;

    public FabricContextExtension(FabricOptions options)
    {
        _options = options;
    }

    public void Initialize(IRegisterOnlyContainer container)
    {
        new FabricContextScaffolding(_options).Register(container);
    }
}
