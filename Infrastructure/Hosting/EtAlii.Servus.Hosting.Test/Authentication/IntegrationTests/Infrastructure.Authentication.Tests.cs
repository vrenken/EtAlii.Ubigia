namespace EtAlii.Servus.Infrastructure.Hosting.Tests.IntegrationTests
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.WebApi;
    using EtAlii.Servus.Tests;
    using Xunit;

    
    public class Infrastructure_Authentication_Tests : IClassFixture<HostUnitTestContext>
    {
        private readonly HostUnitTestContext _testContext;

        public Infrastructure_Authentication_Tests(HostUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Infrastructure_Get_Authentication_Url()
        {
            var configuration = _testContext.HostTestContext.Host.Infrastructure.Configuration;
            var credentials = new NetworkCredential(configuration.Account, configuration.Password);
            string address = _testContext.HostTestContext.Host.AddressFactory.CreateFullAddress(configuration.Address, RelativeUri.Authenticate);
            var token = await _testContext.HostTestContext.Host.Client.Get<string>(address, credentials);
            Assert.True(!String.IsNullOrWhiteSpace(token));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Infrastructure_Get_Authentication_Url_Invalid_Password()
        {
            // Arrange.
            var configuration = _testContext.HostTestContext.Host.Infrastructure.Configuration;
            var credentials = new NetworkCredential(configuration.Account, configuration.Password + "BAAD");
            string address = _testContext.HostTestContext.Host.AddressFactory.CreateFullAddress(configuration.Address, RelativeUri.Authenticate);

            // Act
            var act = _testContext.HostTestContext.Host.Client.Get<string>(address, credentials);

            // Assert.
            await ExceptionAssert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Infrastructure_Get_Authentication_Url_Invalid_Account()
        {
            // Arrange.
            var configuration = _testContext.HostTestContext.Host.Infrastructure.Configuration;
            var credentials = new NetworkCredential(configuration.Account + "BAAD", configuration.Password);
            string address = _testContext.HostTestContext.Host.AddressFactory.CreateFullAddress(configuration.Address, RelativeUri.Authenticate);

            // Act
            var act = _testContext.HostTestContext.Host.Client.Get<string>(address, credentials);

            // Assert.
            await ExceptionAssert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }
    }
}
