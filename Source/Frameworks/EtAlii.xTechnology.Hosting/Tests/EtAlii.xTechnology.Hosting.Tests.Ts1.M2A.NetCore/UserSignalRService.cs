// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.Api.NetCore
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class UserSignalRService : ServiceBase
    {
        public UserSignalRService(IConfigurationSection configuration)
            : base(configuration)
        {
        }

        protected override void ConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder
                .UseCors(builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins($"https://{HostString}");
                })
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapHub<UserHub>($"{nameof(UserHub)}"));
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddRouting()
                .AddCors()
                .AddSignalR(options =>
                {
                    options.EnableDetailedErrors = Debugger.IsAttached;
                })
                .AddJsonProtocol();
        }

    }
}
