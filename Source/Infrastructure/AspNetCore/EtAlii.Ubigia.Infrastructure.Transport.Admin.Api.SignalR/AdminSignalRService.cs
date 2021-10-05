// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.SignalR
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using EtAlii.Ubigia.Infrastructure.Transport.SignalR;
    using EtAlii.Ubigia.Serialization;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.SignalR;

    public class AdminSignalRService : INetworkService
    {
        public Status Status { get; }
        public ServiceConfiguration Configuration { get; }

        public AdminSignalRService(
            ServiceConfiguration configuration, Status status)
        {
            Configuration = configuration;
            Status = status;
        }

        [SuppressMessage(
            category: "Sonar Code Smell",
            checkId: "S4792:Configuring loggers is security-sensitive",
            Justification = "Safe to do so here.")]
        public void ConfigureServices(IServiceCollection services, IServiceProvider globalServices)
        {
            var infrastructure = globalServices.GetService<IInfrastructureService>()!.Infrastructure;

            services
                .AddSingleton(infrastructure.Accounts)
                .AddSingleton(infrastructure.Spaces)
                .AddSingleton(infrastructure.Storages)
                .AddSingleton(infrastructure.Information)

                .AddSingleton(infrastructure.Options) // the service details are needed by the InformationHub.

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

        public void ConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            application
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
