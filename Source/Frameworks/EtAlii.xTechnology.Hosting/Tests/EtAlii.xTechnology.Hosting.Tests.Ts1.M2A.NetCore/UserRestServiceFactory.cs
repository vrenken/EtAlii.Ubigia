// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.Api.NetCore
{
    public class UserRestServiceFactory : IServiceFactory
    {
        public IService Create(ServiceConfiguration configuration, Status status, IHost host) => new UserRestService(configuration, status);
    }
}
