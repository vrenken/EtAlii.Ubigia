// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.SignalR
{
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Transport.SignalR;
    using EtAlii.Ubigia.Serialization;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.SignalR;

    public class AdminSignalRService : ServiceBase
    {
        private readonly IConfigurationDetails _configurationDetails;

        public AdminSignalRService(
            IConfigurationSection configuration,
            IConfigurationDetails configurationDetails)
            : base(configuration)
        {
            _configurationDetails = configurationDetails;
        }

        public override async Task Start()
        {
            Status.Title = "Ubigia infrastructure admin SignalR access";

            Status.Description = "Starting...";
            Status.Summary = "Starting Ubigia admin SignalR services";

            await base.Start().ConfigureAwait(false);

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

            await base.Stop().ConfigureAwait(false);

            var sb = new StringBuilder();
            sb.AppendLine("Finished providing Ubigia admin SignalR services on the address specified below.");
            sb.AppendLine($"Address: {HostString}{PathString}");

            Status.Description = "Stopped";
            Status.Summary = sb.ToString();
        }

        [SuppressMessage("Sonar Code Smell", "S4792:Configuring loggers is security-sensitive", Justification = "Safe to do so here.")]
        protected override void ConfigureServices(IServiceCollection services)
        {
            var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;

            services
                .AddSingleton(infrastructure.Accounts)
                .AddSingleton(infrastructure.Spaces)
                .AddSingleton(infrastructure.Storages)
                .AddSingleton(infrastructure.Information)

                .AddSingleton(_configurationDetails) // the configuration details are needed by the InformationController.

                .AddSignalRInfrastructureAuthentication(infrastructure)
                .AddInfrastructureSerialization()

                .AddRouting()
                .AddCors()
                .AddSignalR(options =>
                {
                    options.AddFilter(new CorrelationServiceHubFilter(infrastructure.ContextCorrelator));

                    // SonarQube: Make sure that this logger's configuration is safe.
                    // As we only add the logging services this ought to be safe. It is when and how they are configured that matters.
                    if (Debugger.IsAttached)
                    {
                        options.EnableDetailedErrors = true;
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
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins($"https://{HostString}");
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
