namespace EtAlii.Ubigia.Infrastructure.Hosting.IntegrationTests
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using Xunit;
    using RelativeUri = EtAlii.Ubigia.Infrastructure.Transport.AspNetCore.RelativeUri;


	public class Infrastructure_Storage_Tests : IClassFixture<HostUnitTestContext>
	{
	    private readonly HostUnitTestContext _testContext;

        public Infrastructure_Storage_Tests(HostUnitTestContext testContext)
        {
	        _testContext = testContext;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Infrastructure_Get_Storage_Local_With_Authentication()
        {
			// Arrange.
			var context = _testContext.HostTestContext;
            var credentials = new NetworkCredential(context.TestAccountName, context.TestAccountPassword);
	        var addressFactory = new AddressFactory();
            var address = addressFactory.CreateFullAddress(context.HostAddress, RelativeUri.Authenticate);
            var token = await context.Host.Client.Get<string>(address, credentials);
            Assert.True(!String.IsNullOrWhiteSpace(token));
            context.Host.Client.AuthenticationToken = token;
            address = addressFactory.CreateFullAddress(context.HostAddress, RelativeUri.Admin.Api.Storages) + "?local";
            
            // Act.
            var storage = context.Host.Client.Get<Storage>(address);

            // Assert.
            Assert.NotNull(storage);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Infrastructure_Get_Storage_Local_Without_Authentication()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
	        var addressFactory = new AddressFactory();
            var address = addressFactory.CreateFullAddress(context.HostAddress, RelativeUri.Admin.Api.Storages) + "?local";

            // Act.
            var act = new Func<Task>(async () => await context.Host.Client.Get<Storage>(address));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [Fact(Skip = "Not working (yet)"), Trait("Category", TestAssembly.Category)]
        public async Task Infrastructure_Get_Storage_Delayed()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var credentials = new NetworkCredential(context.TestAccountName, context.TestAccountPassword);
	        var addressFactory = new AddressFactory();
            var address = addressFactory.CreateFullAddress(context.HostAddress, RelativeUri.Authenticate);
            var token = await context.Host.Client.Get<string>(address, credentials);
            Assert.True(!String.IsNullOrWhiteSpace(token));
            context.Host.Client.AuthenticationToken = token;
            Thread.Sleep(50000);
            address = addressFactory.CreateFullAddress(context.HostAddress, RelativeUri.Admin.Api.Storages) + "?local";
            
            // Act.
            var storage = context.Host.Client.Get<Storage>(address);
            
            // Assert.
            Assert.NotNull(storage);
        }


        [Fact(Skip = "Not working (yet)"), Trait("Category", TestAssembly.Category)]
        public void Infrastructure_Get_Storage_Delayed_Without_Authentication()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
	        var addressFactory = new AddressFactory();
            var address = addressFactory.CreateFullAddress(context.HostAddress, RelativeUri.Admin.Api.Storages) + "?local";

            // Act.
            var act = new Action(() =>
            {
                Thread.Sleep(50000);
                var storage = context.Host.Client.Get<Storage>(address);
                Assert.NotNull(storage);
            });

            // Assert.
            Assert.Throws<InvalidInfrastructureOperationException>(act);
        }
    }
}
