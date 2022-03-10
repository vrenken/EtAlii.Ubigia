// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    using System.Collections.Generic;
    using EtAlii.xTechnology.Hosting;

    public class System1HostServicesFactory : HostServicesFactoryBase
    {
        public override IService[] Create(HostOptions options)
        {
            var services = new List<IService>();

            TryAddService(services, options, "Storage");

            TryAddService(services, options, "Management-Api-Grpc");
            TryAddService(services, options, "Management-Api-Rest");
            TryAddService(services, options, "Management-Api-SignalR");

            TryAddService(services, options, "User-Api-Grpc");
            TryAddService(services, options, "User-Api-Rest");
            TryAddService(services, options, "User-Api-SignalR");

            return services.ToArray();
        }
    }
}
