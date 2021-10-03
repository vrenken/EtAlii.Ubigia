// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.Api.Grpc
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public class UserService : INetworkService
    {
        public Status Status { get; }
        public ServiceConfiguration Configuration { get; }

        public UserService(ServiceConfiguration configuration, Status status)
        {
            Configuration = configuration;
            Status = status;
        }

        public void ConfigureServices(IServiceCollection services, IServiceProvider globalServices) => services.AddGrpc();

        public void ConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            // applicationBuilder.IsolatedMapWhen(
            //     context => context.Request.Host.Port == Port,// && context.Request.Path.StartsWithSegments("/user/api"),
            application
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapGrpcService<UserGrpcService>());
        }
    }
}
