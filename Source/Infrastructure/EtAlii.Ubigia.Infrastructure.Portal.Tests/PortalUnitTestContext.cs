// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Portal.Tests;

using System.Threading;
using EtAlii.xTechnology.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class PortalUnitTestContext
{
    public void ConfigureServices(
        IServiceCollection services,
        IService[] applicationServices,
        IBackgroundService storageService,
        IBackgroundService infrastructureService)
    {
        storageService.ConfigureServices(services, applicationServices);
        storageService.StartAsync(CancellationToken.None).ConfigureAwait(false);
        services.AddSingleton(storageService);

        infrastructureService.ConfigureServices(services, applicationServices);
        infrastructureService.StartAsync(CancellationToken.None).ConfigureAwait(false);
        services.AddSingleton(infrastructureService);
    }

    public IHost RunForConfigureServices(
        IService[] applicationServices,
        IBackgroundService storageService,
        IBackgroundService infrastructureService,
        INetworkService service)
    {
        var host = Host
            .CreateDefaultBuilder()
            .ConfigureWebHost(webHostBuilder => webHostBuilder
                .UseTestServer()
                .ConfigureServices((_, services) =>
                {
                    ConfigureServices(services, applicationServices, storageService, infrastructureService);
                })
                .Configure(builder => builder.Isolate(_ => { }, services => service.ConfigureServices(services, builder.ApplicationServices))))
            .Build();

        return host;
    }

    public IHost RunForConfigureApplication(
        IService[] applicationServices,
        IBackgroundService storageService,
        IBackgroundService infrastructureService,
        INetworkService service)
    {
        var hostBuilder = Host
            .CreateDefaultBuilder();
        var host = hostBuilder
            .ConfigureWebHost(webHostBuilder =>
            {
                // The IsolateMapOnCondition invocation below calls INetworkService.ConfigureApplication.
                webHostBuilder
                    .UseTestServer()
                    .ConfigureServices((_, services) =>
                    {
                        ConfigureServices(services, applicationServices, storageService, infrastructureService);
                    })
                    .Configure((context, application) => application.IsolatedMapOnCondition(context.HostingEnvironment, service));
            })
            .Build();

        return host;
    }
}
