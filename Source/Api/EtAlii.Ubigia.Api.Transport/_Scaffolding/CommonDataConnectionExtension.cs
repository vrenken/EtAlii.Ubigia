// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport;

using EtAlii.xTechnology.MicroContainer;

public class CommonDataConnectionExtension : IExtension
{
    private readonly DataConnectionOptions _options;

    public CommonDataConnectionExtension(DataConnectionOptions options)
    {
        _options = options;
    }
    public void Initialize(IRegisterOnlyContainer container)
    {
        new DataConnectionScaffolding(_options).Register(container);
    }
}
