// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.Grpc
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    public class UserModule : ModuleBase
    {
        public UserModule(IConfigurationSection configuration)
            : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            // Nothing to do here right now...
        }
    }
}
