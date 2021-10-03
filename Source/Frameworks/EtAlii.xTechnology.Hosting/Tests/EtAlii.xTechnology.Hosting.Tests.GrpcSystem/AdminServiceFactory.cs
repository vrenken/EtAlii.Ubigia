// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.GrpcSystem
{
    public class AdminServiceFactory : IServiceFactory
    {
        public IService Create(ServiceConfiguration configuration, Status status, IHost host) => new AdminService(configuration, status);
    }
}
