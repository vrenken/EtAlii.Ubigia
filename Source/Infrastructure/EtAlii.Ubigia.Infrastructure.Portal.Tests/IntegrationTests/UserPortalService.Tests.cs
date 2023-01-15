namespace EtAlii.Ubigia.Infrastructure.Portal.Tests;

using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Portal.Admin;
using EtAlii.xTechnology.Hosting;
using Microsoft.Extensions.Configuration;
using Xunit;


public class UserPortalServiceTests : IClassFixture<PortalUnitTestContext>
{
    private readonly PortalUnitTestContext _testContext;

    public UserPortalServiceTests(PortalUnitTestContext testContext)
    {
        _testContext = testContext;
    }

    [Fact]
    public void UserPortalService_Create()
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
    public async Task UserPortalService_ConfigureServices()
    {
        // Arrange.
        var services = new ServiceInstancesBuilder()
            .AddStorage(out var storageService)
            .AddInfrastructure(out var infrastructureService)
            .AddUserPortal(out var userPortalService)
            .ToServices();

        // Act.
        var host = _testContext.RunForConfigureServices(services, storageService, infrastructureService, userPortalService);
        await host.StartAsync().ConfigureAwait(false);

        // Assert.
        Assert.NotNull(host);

        // Assure.
        await host.StopAsync().ConfigureAwait(false);
    }

    [Fact]
    public async Task UserPortalService_ConfigureApplication()
    {
        // Arrange.
        var services = new ServiceInstancesBuilder()
            .AddStorage(out var storageService)
            .AddInfrastructure(out var infrastructureService)
            .AddUserPortal(out var userPortalService)
            .ToServices();

        // Act.
        var host = _testContext.RunForConfigureApplication(services, storageService, infrastructureService, userPortalService);
        await host.StartAsync().ConfigureAwait(false);

        // Assert.
        Assert.NotNull(host);

        // Assure.
        await host.StopAsync().ConfigureAwait(false);
    }
}
