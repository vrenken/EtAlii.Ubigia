// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.RestSystem
{
    public class UserServiceFactory : IServiceFactory
    {
        public IService Create(ServiceConfiguration configuration, Status status) => new UserService(configuration, status);
    }
}
