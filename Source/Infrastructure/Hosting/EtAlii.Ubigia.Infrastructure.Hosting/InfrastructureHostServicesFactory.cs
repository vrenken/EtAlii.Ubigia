// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System.Collections.Generic;
    using EtAlii.xTechnology.Hosting;

    public class InfrastructureHostServicesFactory : HostServicesFactoryBase
    {
        public override IService[] Create(IHostOptions options, IHost host)
        {
            var services = new List<IService>();

            TryAddService(services, host, options, "Storage");
            TryAddService(services, host, options, "Infrastructure");
            TryAddService(services, host, options, "User-Api-Grpc");
            TryAddService(services, host, options, "User-Api-SignalR");
            TryAddService(services, host, options, "User-Api-Rest");
            TryAddService(services, host, options, "Management-Portal");
            TryAddService(services, host, options, "Management-Api-Grpc");
            TryAddService(services, host, options, "Management-Api-SignalR");
            TryAddService(services, host, options, "Management-Api-Rest");

            return services.ToArray();
        }
    }
}
