// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Portal.Tests;

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Transport;
using EtAlii.xTechnology.Hosting;
using EtAlii.xTechnology.Hosting.Diagnostics;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

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

    public IHost RunConfigureServices(
        ServicesTestConfiguration configuration,
        IBackgroundService storageService,
        IBackgroundService infrastructureService,
        INetworkService service)
    {
        ((InfrastructureService)infrastructureService).SetAlternativeSystemStatusChecker(configuration.AlternativeSystemStatusChecker);

        var host = Host
            .CreateDefaultBuilder()
            .ConfigureWebHost(webHostBuilder => webHostBuilder
                .UseTestServer()
                .ConfigureServices((_, services) =>
                {
                    ConfigureServices(services, configuration.Services, storageService, infrastructureService);
                })
                .Configure(builder => builder.Isolate(_ => { }, services => service.ConfigureServices(services, builder.ApplicationServices))))
            .Build();

        return host;
    }

    public IHost RunConfigureApplication(
        ServicesTestConfiguration configuration,
        IBackgroundService storageService,
        IBackgroundService infrastructureService,
        INetworkService service)
    {
        ((InfrastructureService)infrastructureService).SetAlternativeSystemStatusChecker(configuration.AlternativeSystemStatusChecker);

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
                        ConfigureServices(services, configuration.Services, storageService, infrastructureService);
                    })
                    .Configure((context, application) => application.IsolatedMapOnCondition(context.HostingEnvironment, service));
            })
            .Build();

        return host;
    }

    public IHost SetupHost(ServicesTestConfiguration configuration)
    {
        var host = Host
            .CreateDefaultBuilder()
            .UseHostLogging(configuration.ConfigurationRoot, GetType().Assembly)
            .UseExistingHostTestServices(configuration.ConfigurationRoot, configuration.Services)
            .ConfigureServices(services =>
            {
                var infrastructureServices = services
                    .Where(sd => sd.ServiceType == typeof(IInfrastructureService))
                    .Select(sd => sd.ImplementationInstance)
                    .Cast<InfrastructureService>()
                    .Single();
                infrastructureServices.SetAlternativeSystemStatusChecker(configuration.AlternativeSystemStatusChecker);
            })
            .Build();
        return host;
    }

    public async Task AssertUrlIsActiveByTitle(IHost host, string url, string title)
    {
        using var client = host.GetTestClient();
        string content;
        try
        {
            content = await client
                .GetStringAsync(url)
                .ConfigureAwait(false);
        }
        catch (HttpRequestException e) when(e.StatusCode == HttpStatusCode.NotFound)
        {
            throw new InvalidOperationException($"No website is available at `{url}`");
        }

        var document = new HtmlDocument();
        document.LoadHtml(content);

        Assert.NotNull(document);
        Assert.Equal(title, document.DocumentNode.SelectSingleNode("//head/title").InnerText);
    }

    public async Task AssertUrlIsInactive(IHost host, string url)
    {
        var act = new Func<Task>(async () =>
        {
            using var client = host.GetTestClient();

            await client
                .GetStringAsync(url)
                .ConfigureAwait(false);
        });

        await Assert.ThrowsAsync<HttpRequestException>(act).ConfigureAwait(false);
    }
}
