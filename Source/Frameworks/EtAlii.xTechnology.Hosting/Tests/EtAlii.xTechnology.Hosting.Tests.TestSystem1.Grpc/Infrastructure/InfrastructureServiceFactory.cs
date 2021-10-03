// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Grpc
{
    public class InfrastructureServiceFactory : IServiceFactory
    {
        public IService Create(ServiceConfiguration configuration, Status status) => new InfrastructureService(configuration, status);
    }
}
