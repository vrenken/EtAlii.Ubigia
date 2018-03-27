namespace EtAlii.Ubigia.Infrastructure.Hosting.IntegrationTests
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using Xunit;
    using RelativeUri = EtAlii.Ubigia.Infrastructure.Transport.AspNetCore.RelativeUri;

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
			// Arrange.
	        var context = _testContext.HostTestContext;
            var credentials = new NetworkCredential(context.TestAccountName, context.TestAccountPassword);
	        var addressFactory = new AddressFactory();
            var address = addressFactory.CreateFullAddress(context.HostAddress, RelativeUri.Authenticate);
	        var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

	        // Act.
			var token = await client.Get<string>(address, credentials);

			// Assert.
	        Assert.True(!String.IsNullOrWhiteSpace(token));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Infrastructure_Get_Authentication_Url_Invalid_Password()
        {
            // Arrange.
	        var context = _testContext.HostTestContext;
            var credentials = new NetworkCredential(context.TestAccountName, context.TestAccountPassword + "BAAD");
	        var addressFactory = new AddressFactory();
            var address = addressFactory.CreateFullAddress(context.HostAddress, RelativeUri.Authenticate);
	        var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

			// Act
			var act = new Func<Task>(async () => await client.Get<string>(address, credentials));

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Infrastructure_Get_Authentication_Url_Invalid_Account()
        {
			// Arrange.
			var context = _testContext.HostTestContext;
            var credentials = new NetworkCredential(context.TestAccountName + "BAAD", context.TestAccountPassword);
	        var addressFactory = new AddressFactory();
            var address = addressFactory.CreateFullAddress(context.HostAddress, RelativeUri.Authenticate);
	        var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

			// Act
			var act = new Func<Task>(async () => await client.Get<string>(address, credentials));

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }
    }
}
