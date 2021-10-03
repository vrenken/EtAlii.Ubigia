// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.GrpcSystem
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using WireUserGrpcService = global::EtAlii.xTechnology.Hosting.Tests.GrpcSystem.WireProtocol.UserGrpcService;

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
            application
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapGrpcService<UserGrpcService>());
        }
    }
}
