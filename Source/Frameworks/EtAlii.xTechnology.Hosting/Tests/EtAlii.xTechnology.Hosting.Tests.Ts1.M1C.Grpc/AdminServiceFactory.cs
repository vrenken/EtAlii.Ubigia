// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Admin.Api.Grpc
{
    public class AdminServiceFactory : IServiceFactory
    {
        public IService Create(ServiceConfiguration configuration) => new AdminService(configuration);
    }
}
