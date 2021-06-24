// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.NetCore
{
    using System.Text;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;

    public class UserModule : ModuleBase
    {
        public UserModule(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            Status.Title = "User";
            
            var sb = new StringBuilder();
            sb.AppendLine($"Host: {HostString.Host}");
            sb.AppendLine($"Port: {HostString.Port}");
            
            Status.Summary = Status.Description = sb.ToString();
        }
    }
}
