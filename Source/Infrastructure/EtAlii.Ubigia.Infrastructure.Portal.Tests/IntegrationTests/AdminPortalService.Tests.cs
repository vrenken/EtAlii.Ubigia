namespace EtAlii.Ubigia.Infrastructure.Portal.Tests;

using System.Threading;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Fabric.InMemory;
using EtAlii.Ubigia.Infrastructure.Portal.Admin;
using EtAlii.Ubigia.Infrastructure.Transport;
using EtAlii.xTechnology.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;


public class AdminPortalServiceTests
{
    [Fact]
    public void AdminPortalService_Create()
    {
        // Arrange.
        var configurationRoot = new ConfigurationBuilder()
            .AddJsonFile("HostSettings.json")
            .ExpandEnvironmentVariablesInJson()
            .Build();

        var configurationSection = configurationRoot.GetSection("Management-Portal");
        ServiceConfiguration.TryCreate(configurationSection, configurationRoot, out var configuration);

        // Act.
        var service = new AdminPortalService(configuration);

        // Assert.
        Assert.NotNull(service);
    }

    private void CreateApplicationServices(
        out IBackgroundService storageService,
        out IBackgroundService infrastructureService,
        out IService[] applicationServices,
        out INetworkService service)
    {
        var configurationRoot = new ConfigurationBuilder()
            .AddJsonFile("HostSettings.json")
            .ExpandEnvironmentVariablesInJson()
            .Build();

        var configurationSection = configurationRoot.GetSection("Management-Portal");
        ServiceConfiguration.TryCreate(configurationSection, configurationRoot, out var configuration);
        service = new AdminPortalService(configuration);

        configurationSection = configurationRoot.GetSection("Storage");
        ServiceConfiguration.TryCreate(configurationSection, configurationRoot, out configuration);
        storageService = (IBackgroundService)new StorageServiceFactory().Create(configuration);

        configurationSection = configurationRoot.GetSection("Infrastructure");
        ServiceConfiguration.TryCreate(configurationSection, configurationRoot, out configuration);
        infrastructureService = (IBackgroundService)new InfrastructureServiceFactory().Create(configuration);

        applicationServices = new IService[] { storageService, infrastructureService, service };

    }
    private void ConfigureServices(
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

    [Fact]
    public async Task AdminPortalService_ConfigureServices()
    {
        // Arrange.
        CreateApplicationServices(
            out var storageService,
            out var infrastructureService,
            out var applicationServices,
            out var service);

        // Act.
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
        await host.StartAsync().ConfigureAwait(false);

        // Assert.
        Assert.NotNull(host);

        // Assure.
        await host.StopAsync().ConfigureAwait(false);
    }

    [Fact]
    public async Task AdminPortalService_ConfigureApplication()
    {
        // Arrange.
        CreateApplicationServices(
            out var storageService,
            out var infrastructureService,
            out var applicationServices,
            out var service);

        // Act.
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
        await host.StartAsync().ConfigureAwait(false);

        // Assert.
        Assert.NotNull(host);

        // Assure.
        await host.StopAsync().ConfigureAwait(false);
    }
}
