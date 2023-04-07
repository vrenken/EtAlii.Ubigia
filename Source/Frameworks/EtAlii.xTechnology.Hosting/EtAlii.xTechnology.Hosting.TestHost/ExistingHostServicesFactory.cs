// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting;

using Microsoft.Extensions.Configuration;

public class ExistingHostServicesFactory : HostServicesFactoryBase
{
    private readonly IService[] _services;

    public ExistingHostServicesFactory(IService[] services)
    {
        _services = services;
    }

    public override IService[] Create(IConfigurationRoot configurationRoot)
    {
        return _services;
    }
}
