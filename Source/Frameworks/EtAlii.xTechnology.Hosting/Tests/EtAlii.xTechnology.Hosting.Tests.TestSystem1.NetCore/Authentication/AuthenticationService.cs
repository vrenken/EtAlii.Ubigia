// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.NetCore
{
    using Microsoft.Extensions.Configuration;

    public class AuthenticationService : ServiceBase
    {
        public AuthenticationService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }
    }
}
