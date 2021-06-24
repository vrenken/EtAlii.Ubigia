// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.Api.Grpc
{
    using EtAlii.xTechnology.Hosting.Service.Grpc;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using WireUserGrpcService = global::EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.Api.Grpc.WireProtocol.UserGrpcService;

    public class UserService : GrpcServiceBase
    {
        public UserService(IConfigurationSection configuration) : base(configuration)
        {
        }

        protected override void ConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            // applicationBuilder.IsolatedMapWhen(
            //     context => context.Request.Host.Port == Port,// && context.Request.Path.StartsWithSegments("/user/api"),
            applicationBuilder
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapGrpcService<UserGrpcService>());
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
        }
    }
}
