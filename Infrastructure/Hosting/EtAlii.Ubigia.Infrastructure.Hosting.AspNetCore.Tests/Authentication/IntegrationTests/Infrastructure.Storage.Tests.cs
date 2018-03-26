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


	public class Infrastructure_Storage_Tests : IDisposable
    {
        private IHostTestContext _hostTestContext;

        public Infrastructure_Storage_Tests()
        {
            _hostTestContext = new HostTestContextFactory().Create();
            _hostTestContext.Start();
        }

        public void Dispose()
        {
            _hostTestContext.Stop();
            _hostTestContext = null;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Infrastructure_Get_Storage_Local_With_Authentication()
        {
			// Arrange.
			var context = _hostTestContext;
            var configuration = _hostTestContext.Host.Infrastructure.Configuration;
            var credentials = new NetworkCredential(context.TestAccountName, context.TestAccountPassword);
	        var addressFactory = new AddressFactory();
            var address = addressFactory.CreateFullAddress(configuration.Address, RelativeUri.Authenticate);
            var token = await _hostTestContext.Host.Client.Get<string>(address, credentials);
            Assert.True(!String.IsNullOrWhiteSpace(token));
            _hostTestContext.Host.Client.AuthenticationToken = token;
            address = addressFactory.CreateFullAddress(configuration.Address, RelativeUri.Admin.Api.Storages) + "?local";
            
            // Act.
            var storage = _hostTestContext.Host.Client.Get<Storage>(address);

            // Assert.
            Assert.NotNull(storage);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Infrastructure_Get_Storage_Local_Without_Authentication()
        {
			// Arrange.
            var configuration = _hostTestContext.Host.Infrastructure.Configuration;
	        var addressFactory = new AddressFactory();
            var address = addressFactory.CreateFullAddress(configuration.Address, RelativeUri.Admin.Api.Storages) + "?local";

            // Act.
            var act = new Func<Task>(async () => await _hostTestContext.Host.Client.Get<Storage>(address));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [Fact(Skip = "Not working (yet)"), Trait("Category", TestAssembly.Category)]
        public async Task Infrastructure_Get_Storage_Delayed()
        {
			// Arrange.
			var context = _hostTestContext;
            var configuration = _hostTestContext.Host.Infrastructure.Configuration;
            var credentials = new NetworkCredential(context.TestAccountName, context.TestAccountPassword);
	        var addressFactory = new AddressFactory();
            var address = addressFactory.CreateFullAddress(configuration.Address, RelativeUri.Authenticate);
            var token = await _hostTestContext.Host.Client.Get<string>(address, credentials);
            Assert.True(!String.IsNullOrWhiteSpace(token));
            _hostTestContext.Host.Client.AuthenticationToken = token;
            Thread.Sleep(50000);
            address = addressFactory.CreateFullAddress(configuration.Address, RelativeUri.Admin.Api.Storages) + "?local";
            
            // Act.
            var storage = _hostTestContext.Host.Client.Get<Storage>(address);
            
            // Assert.
            Assert.NotNull(storage);
        }


        [Fact(Skip = "Not working (yet)"), Trait("Category", TestAssembly.Category)]
        public void Infrastructure_Get_Storage_Delayed_Without_Authentication()
        {
			// Arrange.
            var configuration = _hostTestContext.Host.Infrastructure.Configuration;
	        var addressFactory = new AddressFactory();
            var address = addressFactory.CreateFullAddress(configuration.Address, RelativeUri.Admin.Api.Storages) + "?local";

            // Act.
            var act = new Action(() =>
            {
                Thread.Sleep(50000);
                var storage = _hostTestContext.Host.Client.Get<Storage>(address);
                Assert.NotNull(storage);
            });

            // Assert.
            Assert.Throws<InvalidInfrastructureOperationException>(act);
        }
    }
}
