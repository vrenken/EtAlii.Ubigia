namespace EtAlii.Ubigia.Infrastructure.Portal.Tests;

using System.Net.Http;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.Ubigia.Infrastructure.Portal.Admin;
using EtAlii.Ubigia.Tests;
using EtAlii.xTechnology.Hosting;
using Microsoft.Extensions.Configuration;
using Xunit;


[CorrelateUnitTests]
public class SetupPortalServiceTests : IClassFixture<PortalUnitTestContext>
{
    private readonly PortalUnitTestContext _testContext;

    public SetupPortalServiceTests(PortalUnitTestContext testContext)
    {
        _testContext = testContext;
    }

    [Fact]
    public void SetupPortalService_Create()
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
    public async Task SetupPortalService_ConfigureServices()
    {
        // Arrange.
        var configuration = new ServicesTestConfigurationBuilder()
            .AddStorage(out var storageService)
            .AddInfrastructure(out var infrastructureService)
            .AddSetupPortal(out var setupPortalService)
            .SetSystemStatus(SystemStatus.SystemIsOperational)
            .ToConfiguration();

        // Act.
        var host = _testContext.RunConfigureServices(configuration, storageService, infrastructureService, setupPortalService);
        await host.StartAsync().ConfigureAwait(false);

        // Assert.
        Assert.NotNull(host);

        // Assure.
        await host.StopAsync().ConfigureAwait(false);
    }

    [Fact]
    public async Task SetupPortalService_ConfigureApplication()
    {
        // Arrange.
        var configuration = new ServicesTestConfigurationBuilder()
            .AddStorage(out var storageService)
            .AddInfrastructure(out var infrastructureService)
            .AddSetupPortal(out var setupPortalService)
            .SetSystemStatus(SystemStatus.SystemIsOperational)
            .ToConfiguration();

        // Act.
        var host = _testContext.RunConfigureApplication(configuration, storageService, infrastructureService, setupPortalService);
        await host.StartAsync().ConfigureAwait(false);

        // Assert.
        Assert.NotNull(host);

        // Assure.
        await host.StopAsync().ConfigureAwait(false);
    }

    [Fact]
    public async Task SetupPortalService_When_Operational()
    {
        // Arrange.
        var configuration = new ServicesTestConfigurationBuilder()
            .AddStorage(out _)
            .AddInfrastructure(out _)
            .AddSetupPortal(out var setupPortalService)
            .SetSystemStatus(SystemStatus.SystemIsOperational)
            .ToConfiguration();
        using var client = new HttpClient();

        // Act.
        var host = _testContext.SetupHost(configuration);
        await host.StartAsync().ConfigureAwait(false);

        // Assert.
        Assert.NotNull(host);

        await _testContext
            .AssertUrlIsInactive(host, $"https://localhost:{setupPortalService.Configuration.Port}")
            .ConfigureAwait(false);

        await _testContext
            .AssertUrlIsInactive(host, "https://localhost:81")
            .ConfigureAwait(false);

        await _testContext
            .AssertUrlIsInactive(host, "https://localhost:82")
            .ConfigureAwait(false);

        // Assure.
        await host.StopAsync().ConfigureAwait(false);
    }


    [Fact]
    public async Task SetupPortalService_When_Setup_Is_Needed()
    {
        // Arrange.
        var configuration = new ServicesTestConfigurationBuilder()
            .AddStorage(out _)
            .AddInfrastructure(out _)
            .AddSetupPortal(out var setupPortalService)
            .SetSystemStatus(SystemStatus.SetupIsNeeded)
            .ToConfiguration();
        using var client = new HttpClient();

        // Act.
        var host = _testContext.SetupHost(configuration);
        await host.StartAsync().ConfigureAwait(false);

        // Assert.
        Assert.NotNull(host);


        await _testContext
            .AssertUrlIsActiveByTitle(host, $"https://localhost:{setupPortalService.Configuration.Port}", "Ubigia storage - Setup")
            .ConfigureAwait(false);

        await _testContext
            .AssertUrlIsInactive(host, "https://localhost:81")
            .ConfigureAwait(false);

        await _testContext
            .AssertUrlIsInactive(host, "https://localhost:82")
            .ConfigureAwait(false);

        // Assure.
        await host.StopAsync().ConfigureAwait(false);
    }


    [Fact]
    public async Task SetupPortalService_When_Non_Operational()
    {
        // Arrange.
        var configuration = new ServicesTestConfigurationBuilder()
            .AddStorage(out _)
            .AddInfrastructure(out _)
            .AddSetupPortal(out var setupPortalService)
            .SetSystemStatus(SystemStatus.SystemIsNonOperational)
            .ToConfiguration();
        using var client = new HttpClient();

        // Act.
        var host = _testContext.SetupHost(configuration);
        await host.StartAsync().ConfigureAwait(false);

        // Assert.
        Assert.NotNull(host);

        await _testContext
            .AssertUrlIsInactive(host, $"https://localhost:{setupPortalService.Configuration.Port}")
            .ConfigureAwait(false);

        await _testContext
            .AssertUrlIsInactive(host, $"https://localhost:81")
            .ConfigureAwait(false);

        await _testContext
            .AssertUrlIsInactive(host, "https://localhost:82")
            .ConfigureAwait(false);

        // Assure.
        await host.StopAsync().ConfigureAwait(false);
    }





    [Fact]
    public async Task SetupPortalService_When_Operational_With_Admin_Portal()
    {
        // Arrange.
        var configuration = new ServicesTestConfigurationBuilder()
            .AddStorage(out _)
            .AddInfrastructure(out _)
            .AddSetupPortal(out var setupPortalService)
            .AddAdminPortal(out var adminPortalService)
            .SetSystemStatus(SystemStatus.SystemIsOperational)
            .ToConfiguration();
        using var client = new HttpClient();

        // Act.
        var host = _testContext.SetupHost(configuration);
        await host.StartAsync().ConfigureAwait(false);

        // Assert.
        Assert.NotNull(host);

        await _testContext
            .AssertUrlIsInactive(host, $"https://localhost:{setupPortalService.Configuration.Port}")
            .ConfigureAwait(false);

        await _testContext
            .AssertUrlIsActiveByTitle(host, $"https://localhost:{adminPortalService.Configuration.Port}", "Ubigia storage - Administration")
            .ConfigureAwait(false);

        await _testContext
            .AssertUrlIsInactive(host, "https://localhost:82")
            .ConfigureAwait(false);

        // Assure.
        await host.StopAsync().ConfigureAwait(false);
    }


    [Fact]
    public async Task SetupPortalService_When_Setup_Is_Needed_With_Admin_Portal()
    {
        // Arrange.
        var configuration = new ServicesTestConfigurationBuilder()
            .AddStorage(out _)
            .AddInfrastructure(out _)
            .AddSetupPortal(out var setupPortalService)
            .AddAdminPortal(out var adminPortalService)
            .SetSystemStatus(SystemStatus.SetupIsNeeded)
            .ToConfiguration();
        using var client = new HttpClient();

        // Act.
        var host = _testContext.SetupHost(configuration);
        await host.StartAsync().ConfigureAwait(false);

        // Assert.
        Assert.NotNull(host);

        await _testContext
            .AssertUrlIsActiveByTitle(host, $"https://localhost:{setupPortalService.Configuration.Port}", "Ubigia storage - Setup")
            .ConfigureAwait(false);

        await _testContext
            .AssertUrlIsInactive(host, $"https://localhost:{adminPortalService.Configuration.Port}")
            .ConfigureAwait(false);

        await _testContext
            .AssertUrlIsInactive(host, "https://localhost:82")
            .ConfigureAwait(false);

        // Assure.
        await host.StopAsync().ConfigureAwait(false);
    }


    [Fact]
    public async Task SetupPortalService_When_Non_Operational_With_Admin_Portal()
    {
        // Arrange.
        var configuration = new ServicesTestConfigurationBuilder()
            .AddStorage(out _)
            .AddInfrastructure(out _)
            .AddSetupPortal(out var setupPortalService)
            .AddAdminPortal(out var adminPortalService)
            .SetSystemStatus(SystemStatus.SystemIsNonOperational)
            .ToConfiguration();
        using var client = new HttpClient();

        // Act.
        var host = _testContext.SetupHost(configuration);
        await host.StartAsync().ConfigureAwait(false);

        // Assert.
        Assert.NotNull(host);

        await _testContext
            .AssertUrlIsInactive(host, $"https://localhost:{setupPortalService.Configuration.Port}")
            .ConfigureAwait(false);

        await _testContext
            .AssertUrlIsActiveByTitle(host, $"https://localhost:{adminPortalService.Configuration.Port}", "Ubigia storage - Administration")
            .ConfigureAwait(false);

        await _testContext
            .AssertUrlIsInactive(host, "https://localhost:82")
            .ConfigureAwait(false);

        // Assure.
        await host.StopAsync().ConfigureAwait(false);
    }






    [Fact]
    public async Task SetupPortalService_When_Operational_With_Admin_Portal_And_User_Portal()
    {
        // Arrange.
        var configuration = new ServicesTestConfigurationBuilder()
            .AddStorage(out _)
            .AddInfrastructure(out _)
            .AddSetupPortal(out var setupPortalService)
            .AddAdminPortal(out var adminPortalService)
            .AddUserPortal(out var userPortalService)
            .SetSystemStatus(SystemStatus.SystemIsOperational)
            .ToConfiguration();
        using var client = new HttpClient();

        // Act.
        var host = _testContext.SetupHost(configuration);
        await host.StartAsync().ConfigureAwait(false);

        // Assert.
        Assert.NotNull(host);

        await _testContext
            .AssertUrlIsInactive(host, $"https://localhost:{setupPortalService.Configuration.Port}")
            .ConfigureAwait(false);

        await _testContext
            .AssertUrlIsActiveByTitle(host, $"https://localhost:{adminPortalService.Configuration.Port}", "Ubigia storage - Administration")
            .ConfigureAwait(false);

        await _testContext
            .AssertUrlIsActiveByTitle(host, $"https://localhost:{userPortalService.Configuration.Port}", "Ubigia storage")
            .ConfigureAwait(false);

        // Assure.
        await host.StopAsync().ConfigureAwait(false);
    }


    [Fact]
    public async Task SetupPortalService_When_Setup_Is_Needed_With_Admin_Portal_And_User_Portal()
    {
        // Arrange.
        var configuration = new ServicesTestConfigurationBuilder()
            .AddStorage(out _)
            .AddInfrastructure(out _)
            .AddSetupPortal(out var setupPortalService)
            .AddAdminPortal(out var adminPortalService)
            .AddUserPortal(out var userPortalService)
            .SetSystemStatus(SystemStatus.SetupIsNeeded)
            .ToConfiguration();
        using var client = new HttpClient();

        // Act.
        var host = _testContext.SetupHost(configuration);
        await host.StartAsync().ConfigureAwait(false);

        // Assert.
        Assert.NotNull(host);

        await _testContext
            .AssertUrlIsActiveByTitle(host, $"https://localhost:{setupPortalService.Configuration.Port}", "Ubigia storage - Setup")
            .ConfigureAwait(false);

        await _testContext
            .AssertUrlIsInactive(host, $"https://localhost:{adminPortalService.Configuration.Port}")
            .ConfigureAwait(false);

        await _testContext
            .AssertUrlIsInactive(host, $"https://localhost:{userPortalService.Configuration.Port}")
            .ConfigureAwait(false);

        // Assure.
        await host.StopAsync().ConfigureAwait(false);
    }


    [Fact]
    public async Task SetupPortalService_When_Non_Operational_With_Admin_Portal_And_User_Portal()
    {
        // Arrange.
        var configuration = new ServicesTestConfigurationBuilder()
            .AddStorage(out _)
            .AddInfrastructure(out _)
            .AddSetupPortal(out var setupPortalService)
            .AddAdminPortal(out var adminPortalService)
            .AddUserPortal(out var userPortalService)
            .SetSystemStatus(SystemStatus.SystemIsNonOperational)
            .ToConfiguration();
        using var client = new HttpClient();

        // Act.
        var host = _testContext.SetupHost(configuration);
        await host.StartAsync().ConfigureAwait(false);

        // Assert.
        Assert.NotNull(host);

        await _testContext
            .AssertUrlIsInactive(host, $"https://localhost:{setupPortalService.Configuration.Port}")
            .ConfigureAwait(false);

        await _testContext
            .AssertUrlIsActiveByTitle(host, $"https://localhost:{adminPortalService.Configuration.Port}", "Ubigia storage - Administration")
            .ConfigureAwait(false);

        await _testContext
            .AssertUrlIsInactive(host, $"https://localhost:{userPortalService.Configuration.Port}")
            .ConfigureAwait(false);

        // Assure.
        await host.StopAsync().ConfigureAwait(false);
    }
}
