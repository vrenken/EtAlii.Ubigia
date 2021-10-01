// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System.Collections.Generic;
    using EtAlii.xTechnology.Hosting;

    public class InfrastructureHostServicesFactory : HostServicesFactoryBase
    {
        public override IService[] Create(IHostOptions options)
        {
            var services = new List<IService>();

            TryAddService(services, options, "Storage");
            TryAddService(services, options, "Infrastructure");
            TryAddService(services, options, "User-Api-Grpc");
            TryAddService(services, options, "User-Api-SignalR");
            TryAddService(services, options, "User-Api-Rest");
            TryAddService(services, options, "Management-Portal");
            TryAddService(services, options, "Management-Api-Grpc");
            TryAddService(services, options, "Management-Api-SignalR");
            TryAddService(services, options, "Management-Api-Rest");

            return services.ToArray();
        }
    }
}
