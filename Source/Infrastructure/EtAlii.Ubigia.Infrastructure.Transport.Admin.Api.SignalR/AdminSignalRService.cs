// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.SignalR;

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.Ubigia.Infrastructure.Transport.SignalR;
using EtAlii.Ubigia.Serialization;
using EtAlii.xTechnology.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SignalR;

public class AdminSignalRService : NetworkServiceBase<AdminSignalRService>
{
    public AdminSignalRService(ServiceConfiguration configuration)
        : base(configuration)
    {
    }

    [SuppressMessage(
        category: "Sonar Code Smell",
        checkId: "S4792:Configuring loggers is security-sensitive",
        Justification = "Safe to do so here.")]
    protected override void ConfigureNetworkServices(
        IServiceCollection services,
        IServiceProvider globalServices,
        IFunctionalContext functionalContext)
    {
        services
            .AddSingleton(functionalContext)
            .AddSingleton(functionalContext.Storages)
            .AddSingleton(functionalContext.Spaces)
            .AddSingleton(functionalContext.Accounts)

            .AddSingleton(functionalContext.Information)

            .AddSignalRInfrastructureAuthentication(functionalContext)
            .AddInfrastructureSerialization()

            .AddRouting()
            .AddCors()
            .AddSignalR(options =>
            {
                options.MaximumParallelInvocationsPerClient = 10;
                options.AddFilter(new CorrelationServiceHubFilter(functionalContext.ContextCorrelator));

                // SonarQube: Make sure that this logger's configuration is safe.
                // As we only add the logging services this ought to be safe. It is when and how they are configured that matters.
                if (Debugger.IsAttached)
                {
                    options.EnableDetailedErrors = true;
                }
            })
            .AddNewtonsoftJsonProtocol(options => Serializer.Configure(options.PayloadSerializerSettings));
    }

    protected override void ConfigureNetworkApplication(
        IApplicationBuilder application,
        IWebHostEnvironment environment)
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
