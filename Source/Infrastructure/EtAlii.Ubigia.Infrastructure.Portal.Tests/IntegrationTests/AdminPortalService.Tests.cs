﻿namespace EtAlii.Ubigia.Infrastructure.Portal.Tests;

using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Portal.Admin;
using EtAlii.xTechnology.Hosting;
using Microsoft.Extensions.Configuration;
using Xunit;


public class AdminPortalServiceTests : IClassFixture<PortalUnitTestContext>
{
    private readonly PortalUnitTestContext _testContext;

    public AdminPortalServiceTests(PortalUnitTestContext testContext)
    {
        _testContext = testContext;
    }

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

    [Fact]
    public async Task AdminPortalService_ConfigureServices()
    {
        // Arrange.
        var services = new ServiceInstancesBuilder()
            .AddStorage(out var storageService)
            .AddInfrastructure(out var infrastructureService)
            .AddAdminPortal(out var adminPortalService)
            .ToServices();

        // Act.
        var host = _testContext.RunForConfigureServices(services, storageService, infrastructureService, adminPortalService);
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
        var services = new ServiceInstancesBuilder()
            .AddStorage(out var storageService)
            .AddInfrastructure(out var infrastructureService)
            .AddAdminPortal(out var adminPortalService)
            .ToServices();

        // Act.
        var host = _testContext.RunForConfigureApplication(services, storageService, infrastructureService, adminPortalService);
        await host.StartAsync().ConfigureAwait(false);

        // Assert.
        Assert.NotNull(host);

        // Assure.
        await host.StopAsync().ConfigureAwait(false);
    }
}
