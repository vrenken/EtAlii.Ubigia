// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.SignalRSystem
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class UserService : ServiceBase
    {
        public UserService(IConfigurationSection configuration)
            : base(configuration)
        {
        }

        protected override void ConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder
                .UseCors(builder =>
                {
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins($"https://{HostString}");
                })
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapHub<UserHub>(SignalRHub.User);
                });
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCors()
                .AddSignalR(options => options.EnableDetailedErrors = Debugger.IsAttached);
        }
    }
}

