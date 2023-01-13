// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local;

using System.Collections.Generic;
using EtAlii.xTechnology.Hosting;
using Microsoft.Extensions.Configuration;

public class LocalHostServicesFactory : HostServicesFactoryBase
{
    public override IService[] Create(IConfigurationRoot configurationRoot)
    {
        var services = new List<IService>();

        TryAddService(services, configurationRoot, "Storage");

        TryAddService(services, configurationRoot, "Management-Api-Grpc");
        TryAddService(services, configurationRoot, "Management-Api-Rest");
        TryAddService(services, configurationRoot, "Management-Api-SignalR");

        TryAddService(services, configurationRoot, "User-Api-Grpc");
        TryAddService(services, configurationRoot, "User-Api-Rest");
        TryAddService(services, configurationRoot, "User-Api-SignalR");

        return services.ToArray();
    }
}
