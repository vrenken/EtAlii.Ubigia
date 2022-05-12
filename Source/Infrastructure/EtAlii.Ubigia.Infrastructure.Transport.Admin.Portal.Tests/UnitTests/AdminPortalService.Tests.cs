namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.Tests
{
    using EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Hosting;
    using Xunit;

    public class AdminPortalServiceTests
    {

        [Fact]
        public void AdminPortalService_Create()
        {
            // Arrange.
            var configuration = new ServiceConfiguration();

            // Act.
            var service = new AdminPortalService(configuration);

            // Assert.
            Assert.NotNull(service);
        }

        [Fact]
        public void AdminPortalService_ConfigureServices()
        {
            // Arrange.
            var configuration = new ServiceConfiguration();
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

            // Assert.
            Assert.NotNull(host);
        }
    }
}
