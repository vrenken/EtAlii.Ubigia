// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Admin.NetCore
{
    using System.Text;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    public class AdminModule : ModuleBase
    {
        public AdminModule(IConfigurationSection configuration)
            : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            Status.Title = "Admin";

            var sb = new StringBuilder();
            sb.AppendLine($"Host: {HostString.Host}");
            sb.AppendLine($"Port: {HostString.Port}");

            Status.Summary = Status.Description = sb.ToString();
        }
    }
}
