// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management;

using EtAlii.xTechnology.MicroContainer;

public sealed class CommonManagementConnectionExtension : IExtension
{
    private readonly ManagementConnectionOptions _options;

    public CommonManagementConnectionExtension(ManagementConnectionOptions options)
    {
        _options = options;
    }

    public void Initialize(IRegisterOnlyContainer container)
    {
        new ManagementConnectionScaffolding(_options).Register(container);
    }
}
