// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Admin.Api.Grpc
{
    using System.Text;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting.Service.Grpc;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using WireAdminGrpcService = global::EtAlii.xTechnology.Hosting.Tests.Infrastructure.Admin.Api.Grpc.WireProtocol.AdminGrpcService;

    public class AdminService : GrpcServiceBase
    {
        public AdminService(IConfigurationSection configuration)
            : base(configuration)
        {
        }

        public override async Task Start()
        {
            Status.Title = "Test system 1 module 1 admin gRPC access";

            Status.Description = "Starting Test admin gRPC services";
            Status.Summary = "Starting...";

            await base.Start().ConfigureAwait(false);

            var sb = new StringBuilder();
            sb.AppendLine("All OK. Test admin gRPC services are available on the address specified below.");
            sb.AppendLine($"Address: {HostString}{PathString}");

            Status.Summary = "Running";
            Status.Description = sb.ToString();
        }

        public override async Task Stop()
        {
            Status.Summary = "Stopping...";
            Status.Description = "Stopping Test admin gRPC services";

            await base.Stop().ConfigureAwait(false);

            var sb = new StringBuilder();
            sb.AppendLine("Finished providing Test admin gRPC services on the address specified below.");
            sb.AppendLine($"Address: {HostString}{PathString}");

            Status.Summary = "Stopped";
            Status.Description = sb.ToString();
        }

        protected override void ConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            // applicationBuilder.IsolatedMapWhen(
            //     context => context.Request.Host.Port == Port,// && context.Request.Path.StartsWithSegments("/admin/api"),
            applicationBuilder
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapGrpcService<AdminGrpcService>());
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
        }
    }
}
