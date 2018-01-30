namespace EtAlii.Ubigia.Infrastructure.Hosting.IntegrationTests
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.WebApi;
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
	        var context = _testContext.HostTestContext;
            var configuration = _testContext.HostTestContext.Host.Infrastructure.Configuration;
            var credentials = new NetworkCredential(context.TestAccountName, context.TestAccountPassword);
            string address = _testContext.HostTestContext.Host.AddressFactory.CreateFullAddress(configuration.Address, RelativeUri.Authenticate);
            var token = await _testContext.HostTestContext.Host.Client.Get<string>(address, credentials);
            Assert.True(!String.IsNullOrWhiteSpace(token));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Infrastructure_Get_Authentication_Url_Invalid_Password()
        {
            // Arrange.
	        var context = _testContext.HostTestContext;
            var configuration = _testContext.HostTestContext.Host.Infrastructure.Configuration;
            var credentials = new NetworkCredential(context.TestAccountName, context.TestAccountPassword + "BAAD");
            string address = _testContext.HostTestContext.Host.AddressFactory.CreateFullAddress(configuration.Address, RelativeUri.Authenticate);

            // Act
            var act = new Func<Task>(async () => await _testContext.HostTestContext.Host.Client.Get<string>(address, credentials));

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Infrastructure_Get_Authentication_Url_Invalid_Account()
        {
			// Arrange.
			var context = _testContext.HostTestContext;
            var configuration = _testContext.HostTestContext.Host.Infrastructure.Configuration;
            var credentials = new NetworkCredential(context.TestAccountName, context.TestAccountPassword);
            string address = _testContext.HostTestContext.Host.AddressFactory.CreateFullAddress(configuration.Address, RelativeUri.Authenticate);

            // Act
            var act = new Func<Task>(async () => await _testContext.HostTestContext.Host.Client.Get<string>(address, credentials));

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }
    }
}
