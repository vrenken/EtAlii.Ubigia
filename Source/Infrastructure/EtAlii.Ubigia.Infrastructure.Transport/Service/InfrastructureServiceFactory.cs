// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.xTechnology.Hosting;

    public class InfrastructureServiceFactory : IServiceFactory
    {
        public IService Create(ServiceConfiguration configuration, Status status, IHost host) => new InfrastructureService(configuration, status, host);
    }
}
