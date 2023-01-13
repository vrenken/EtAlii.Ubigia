// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System.Collections.Generic;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public class InfrastructureHostServicesFactory : HostServicesFactoryBase
    {
        public override IService[] Create(IConfigurationRoot configurationRoot)
        {
            var services = new List<IService>();

            // Core internal services.
            TryAddService(services, configurationRoot, "Storage");
            TryAddService(services, configurationRoot, "Infrastructure");

            // Core user network services.
            TryAddService(services, configurationRoot, "User-Api-Grpc");
            TryAddService(services, configurationRoot, "User-Api-SignalR");
            TryAddService(services, configurationRoot, "User-Api-Rest");

            // Core management network services.
            TryAddService(services, configurationRoot, "Management-Portal");
            TryAddService(services, configurationRoot, "Management-Api-Grpc");
            TryAddService(services, configurationRoot, "Management-Api-SignalR");
            TryAddService(services, configurationRoot, "Management-Api-Rest");

            // Setup network service (will only be enabled when the basic system settings are missing).
            TryAddService(services, configurationRoot, "Setup-Portal");

            return services.ToArray();
        }
    }
}
