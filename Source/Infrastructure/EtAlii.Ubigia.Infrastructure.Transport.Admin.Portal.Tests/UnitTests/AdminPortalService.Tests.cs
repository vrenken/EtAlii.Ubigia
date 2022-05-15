namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
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

        [Fact]
        public async Task AdminPortalService_ConfigureServices()
        {
            // Arrange.
            var configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("HostSettings.json")
                .ExpandEnvironmentVariablesInJson()
                .Build();

            var configurationSection = configurationRoot.GetSection("Management-Portal");
            ServiceConfiguration.TryCreate(configurationSection, configurationRoot, out var configuration);

            var service = new AdminPortalService(configuration);

            // Act.
            var host = Host
                .CreateDefaultBuilder()
                .ConfigureWebHost(webHostBuilder =>
                {
                    webHostBuilder
                        .UseTestServer()
                        .Configure(builder => { builder.Isolate(_ => { }, services => { service.ConfigureServices(services, builder.ApplicationServices); }); });
                })
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
            var configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("HostSettings.json")
                .ExpandEnvironmentVariablesInJson()
                .Build();

            var configurationSection = configurationRoot.GetSection("Management-Portal");
            ServiceConfiguration.TryCreate(configurationSection, configurationRoot, out var configuration);

            var service = new AdminPortalService(configuration);

            // Act.
            var hostBuilder = Host
                .CreateDefaultBuilder();
            var host = hostBuilder
                .ConfigureWebHost(webHostBuilder =>
                {
                    webHostBuilder
                        .UseTestServer()
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
}
