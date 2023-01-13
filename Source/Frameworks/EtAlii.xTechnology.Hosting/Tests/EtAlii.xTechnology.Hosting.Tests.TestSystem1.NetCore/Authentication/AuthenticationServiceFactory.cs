// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.NetCore;

public class AuthenticationServiceFactory : IServiceFactory
{
    public IService Create(ServiceConfiguration configuration) => new AuthenticationService(configuration);
}
