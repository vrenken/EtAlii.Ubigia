﻿namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.SignalR
{
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Transport.NetCore;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminSignalRService : ServiceBase
    {
        private readonly IConfigurationDetails _configurationDetails;

        public AdminSignalRService(IConfigurationSection configuration, IConfigurationDetails configurationDetails) 
            : base(configuration)
        {
            _configurationDetails = configurationDetails;
        }

        public override async Task Start()
        {
            Status.Title = "Ubigia infrastructure admin SignalR access";

            Status.Description = "Starting...";
            Status.Summary = "Starting Ubigia admin SignalR services";

            await base.Start();

            var sb = new StringBuilder();
            sb.AppendLine("All OK. Ubigia admin SignalR services are available on the address specified below.");
            sb.AppendLine($"Address: {HostString}{PathString}");

            Status.Description = "Running";
            Status.Summary = sb.ToString();
        }

        public override async Task Stop()
        {
            Status.Description = "Stopping...";
            Status.Summary = "Stopping Ubigia admin SignalR services";

            await base.Stop();

            var sb = new StringBuilder();
            sb.AppendLine("Finished providing Ubigia admin SignalR services on the address specified below.");
            sb.AppendLine($"Address: {HostString}{PathString}");

            Status.Description = "Stopped";
            Status.Summary = sb.ToString();
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;
            services
                .AddSingleton(infrastructure.Accounts)
                .AddSingleton(infrastructure.Spaces)
                .AddSingleton(infrastructure.Storages)
                .AddSingleton(infrastructure.Information)

                .AddSingleton(_configurationDetails) // the configuration details are needed by the InformationController.

                .AddInfrastructureAuthentication(infrastructure)
                .AddInfrastructureSerialization()

                .AddRouting()
                .AddCors()
                .AddSignalR(options => 
                {
                    if (Debugger.IsAttached)
                    {
                        options.EnableDetailedErrors = Debugger.IsAttached;
                    }
                })
                .AddNewtonsoftJsonProtocol(options => SerializerFactory.Configure(options.PayloadSerializerSettings));
        }

        protected override void ConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder
                .UseCors(builder =>
                {
                    builder
                        .AllowAnyOrigin() 
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins($"http://{HostString}");
                })
                .UseRouting()
                .UseEndpoints(endPoints =>
                {
                    endPoints.MapHub<AuthenticationHub>(SignalRHub.Authentication);

                    endPoints.MapHub<StorageHub>(SignalRHub.Storage);
                    endPoints.MapHub<SpaceHub>(SignalRHub.Space);
                    endPoints.MapHub<AccountHub>(SignalRHub.Account);
                    endPoints.MapHub<InformationHub>(SignalRHub.Information);
                });
        }
    }
}