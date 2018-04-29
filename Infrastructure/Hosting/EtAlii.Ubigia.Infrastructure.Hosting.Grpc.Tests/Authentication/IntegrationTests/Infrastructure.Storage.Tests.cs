namespace EtAlii.Ubigia.Infrastructure.Hosting.Grpc.Tests
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using UserAuthenticationClient = EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationGrpcService.AuthenticationGrpcServiceClient;
    using AdminAuthenticationClient = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AuthenticationGrpcService.AuthenticationGrpcServiceClient;
    using UserAuthenticationRequest = EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest;
    using AdminAuthenticationRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AuthenticationRequest;
    using Xunit;
    using RelativeUri = EtAlii.Ubigia.Infrastructure.Transport.Grpc.RelativeUri;


	public class InfrastructureStorageTests : IClassFixture<InfrastructureUnitTestContext>
	{
	    private readonly InfrastructureUnitTestContext _testContext;

        public InfrastructureStorageTests(InfrastructureUnitTestContext testContext)
        {
	        _testContext = testContext;
        }

		[Fact, Trait("Category", TestAssembly.Category)]
		public async Task Infrastructure_Get_Storage_Local_Admin_TestUser_With_Authentication()
		{
			// Arrange.
			var context = _testContext.HostTestContext;
			var credentials = new NetworkCredential(context.TestAccountName, context.TestAccountPassword);
			var addressFactory = new AddressFactory();
			var address = addressFactory.Create(context.ManagementServiceAddress, RelativeUri.Authenticate);
			var client = _testContext.HostTestContext.CreateRestInfrastructureClient();
			var token = await client.Get<string>(address, credentials);
			Assert.True(!String.IsNullOrWhiteSpace(token));
			client.AuthenticationToken = token;
			address = addressFactory.Create(context.HostAddress, RelativeUri.Admin.Api.Storages, UriParameter.Local);

			// Act.
			var storage = client.Get<Storage>(address);

			// Assert.
			Assert.NotNull(storage);
		}

		[Fact, Trait("Category", TestAssembly.Category)]
		public async Task Infrastructure_Get_Storage_Local_Admin_Admin_With_Authentication()
		{
			// Arrange.
			var context = _testContext.HostTestContext;
			var credentials = new NetworkCredential(context.AdminAccountName, context.AdminAccountPassword);
			var addressFactory = new AddressFactory();
			var address = addressFactory.Create(context.ManagementServiceAddress, RelativeUri.Authenticate);
			var client = _testContext.HostTestContext.CreateRestInfrastructureClient();
			var token = await client.Get<string>(address, credentials);
			Assert.True(!String.IsNullOrWhiteSpace(token));
			client.AuthenticationToken = token;
			address = addressFactory.Create(context.HostAddress, RelativeUri.Admin.Api.Storages, UriParameter.Local);

			// Act.
			var storage = client.Get<Storage>(address);

			// Assert.
			Assert.NotNull(storage);
		}

		[Fact, Trait("Category", TestAssembly.Category)]
		public async Task Infrastructure_Get_Storage_Local_Admin_System_With_Authentication()
		{
			// Arrange.
			var context = _testContext.HostTestContext;
			var credentials = new NetworkCredential(context.SystemAccountName, context.SystemAccountPassword);
			var addressFactory = new AddressFactory();
			var address = addressFactory.Create(context.ManagementServiceAddress, RelativeUri.Authenticate);
			var client = _testContext.HostTestContext.CreateRestInfrastructureClient();
			var token = await client.Get<string>(address, credentials);
			Assert.True(!String.IsNullOrWhiteSpace(token));
			client.AuthenticationToken = token;
			address = addressFactory.Create(context.HostAddress, RelativeUri.Admin.Api.Storages, UriParameter.Local);

			// Act.
			var storage = client.Get<Storage>(address);

			// Assert.
			Assert.NotNull(storage);
		}

		[Fact, Trait("Category", TestAssembly.Category)]
        public async Task Infrastructure_Get_Storage_Local_Without_Authentication()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
	        var addressFactory = new AddressFactory();
            var address = addressFactory.Create(context.HostAddress, RelativeUri.Admin.Api.Storages, UriParameter.Local);
	        var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

			// Act.
			var act = new Func<Task>(async () => await client.Get<Storage>(address));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

		[Fact(Skip = "Not working (yet)"), Trait("Category", TestAssembly.Category)]
		public async Task Infrastructure_Get_Storage_Delayed_Admin_TestUser()
		{
			// Arrange.
			var context = _testContext.HostTestContext;
			var credentials = new NetworkCredential(context.TestAccountName, context.TestAccountPassword);
			var addressFactory = new AddressFactory();
			var address = addressFactory.Create(context.HostAddress, RelativeUri.Authenticate);
			var client = _testContext.HostTestContext.CreateRestInfrastructureClient();
			var token = await client.Get<string>(address, credentials);
			Assert.True(!String.IsNullOrWhiteSpace(token));
			client.AuthenticationToken = token;
			Thread.Sleep(50000);
			address = addressFactory.Create(context.HostAddress, RelativeUri.Admin.Api.Storages, UriParameter.Local);

			// Act.
			var storage = client.Get<Storage>(address);

			// Assert.
			Assert.NotNull(storage);
		}

		[Fact(Skip = "Not working (yet)"), Trait("Category", TestAssembly.Category)]
		public async Task Infrastructure_Get_Storage_Delayed_Admin_Admin()
		{
			// Arrange.
			var context = _testContext.HostTestContext;
			var credentials = new NetworkCredential(context.AdminAccountName, context.AdminAccountPassword);
			var addressFactory = new AddressFactory();
			var address = addressFactory.Create(context.HostAddress, RelativeUri.Authenticate);
			var client = _testContext.HostTestContext.CreateRestInfrastructureClient();
			var token = await client.Get<string>(address, credentials);
			Assert.True(!String.IsNullOrWhiteSpace(token));
			client.AuthenticationToken = token;
			Thread.Sleep(50000);
			address = addressFactory.Create(context.HostAddress, RelativeUri.Admin.Api.Storages, UriParameter.Local);

			// Act.
			var storage = client.Get<Storage>(address);

			// Assert.
			Assert.NotNull(storage);
		}

		[Fact(Skip = "Not working (yet)"), Trait("Category", TestAssembly.Category)]
		public async Task Infrastructure_Get_Storage_Delayed_Admin_System()
		{
			// Arrange.
			var context = _testContext.HostTestContext;
			var credentials = new NetworkCredential(context.SystemAccountName, context.SystemAccountPassword);
			var addressFactory = new AddressFactory();
			var address = addressFactory.Create(context.HostAddress, RelativeUri.Authenticate);
			var client = _testContext.HostTestContext.CreateRestInfrastructureClient();
			var token = await client.Get<string>(address, credentials);
			Assert.True(!String.IsNullOrWhiteSpace(token));
			client.AuthenticationToken = token;
			Thread.Sleep(50000);
			address = addressFactory.Create(context.HostAddress, RelativeUri.Admin.Api.Storages, UriParameter.Local);

			// Act.
			var storage = client.Get<Storage>(address);

			// Assert.
			Assert.NotNull(storage);
		}

		[Fact(Skip = "Not working (yet)"), Trait("Category", TestAssembly.Category)]
        public void Infrastructure_Get_Storage_Delayed_Without_Authentication()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
	        var addressFactory = new AddressFactory();
            var address = addressFactory.Create(context.HostAddress, RelativeUri.Admin.Api.Storages, UriParameter.Local);
	        var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

			// Act.
			var act = new Action(() =>
            {
                Thread.Sleep(50000);
                var storage = client.Get<Storage>(address);
                Assert.NotNull(storage);
            });

            // Assert.
            Assert.Throws<InvalidInfrastructureOperationException>(act);
        }
    }
}
