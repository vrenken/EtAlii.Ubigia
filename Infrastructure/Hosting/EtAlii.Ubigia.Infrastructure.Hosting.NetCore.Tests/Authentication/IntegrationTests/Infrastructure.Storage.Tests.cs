﻿namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Net;
	using System.Threading;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Api;
	using EtAlii.Ubigia.Api.Transport;
	using EtAlii.Ubigia.Api.Transport.WebApi;
	using Xunit;
	using RelativeUri = EtAlii.Ubigia.Infrastructure.Transport.NetCore.RelativeUri;

	[Trait("Technology", "NetCore")]
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
			var address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeUri.Authenticate);
			var client = _testContext.HostTestContext.CreateRestInfrastructureClient();
			var token = await client.Get<string>(address, credentials);
			Assert.True(!string.IsNullOrWhiteSpace(token));
			client.AuthenticationToken = token;
			address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeUri.Management.Api.Storages, UriParameter.Local);

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
			var address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeUri.Authenticate);
			var client = _testContext.HostTestContext.CreateRestInfrastructureClient();
			var token = await client.Get<string>(address, credentials);
			Assert.True(!string.IsNullOrWhiteSpace(token));
			client.AuthenticationToken = token;
			address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeUri.Management.Api.Storages, UriParameter.Local);

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
			var address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeUri.Authenticate);
			var client = _testContext.HostTestContext.CreateRestInfrastructureClient();
			var token = await client.Get<string>(address, credentials);
			Assert.True(!string.IsNullOrWhiteSpace(token));
			client.AuthenticationToken = token;
			address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeUri.Management.Api.Storages, UriParameter.Local);

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
			var address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeUri.Management.Api.Storages, UriParameter.Local);
			var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

			// Act.
			var act = new Func<Task>(async () => await client.Get<Storage>(address));

			// Assert.
			await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
		}

		[Fact, Trait("Category", TestAssembly.Category)]
		public async Task Infrastructure_Get_Accounts_Without_Authentication()
		{
			// Arrange.
			var context = _testContext.HostTestContext;
			var addressFactory = new AddressFactory();
			var address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeUri.Management.Api.Accounts);
			var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

			// Act.
			var act = new Func<Task>(async () => await client.Get<IEnumerable<Account>>(address));

			// Assert.
			await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
		}

		[Fact, Trait("Category", TestAssembly.Category)]
		public async Task Infrastructure_Get_Storage_Delayed_Admin_TestUser()
		{
			// Arrange.
			var context = _testContext.HostTestContext;
			var credentials = new NetworkCredential(context.TestAccountName, context.TestAccountPassword);
			var addressFactory = new AddressFactory();
			var address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeUri.Authenticate);
			var client = _testContext.HostTestContext.CreateRestInfrastructureClient();
			var token = await client.Get<string>(address, credentials);
			Assert.True(!string.IsNullOrWhiteSpace(token));
			client.AuthenticationToken = token;
			Thread.Sleep(TimeSpan.FromSeconds(30));
			address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeUri.Management.Api.Storages, UriParameter.Local);

			// Act.
			var storage = client.Get<Storage>(address);

			// Assert.
			Assert.NotNull(storage);
		}

		[Fact, Trait("Category", TestAssembly.Category)]
		public async Task Infrastructure_Get_Storage_Delayed_Admin_Admin()
		{
			// Arrange.
			var context = _testContext.HostTestContext;
			var credentials = new NetworkCredential(context.AdminAccountName, context.AdminAccountPassword);
			var addressFactory = new AddressFactory();
			var address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeUri.Authenticate);
			var client = _testContext.HostTestContext.CreateRestInfrastructureClient();
			var token = await client.Get<string>(address, credentials);
			Assert.True(!string.IsNullOrWhiteSpace(token));
			client.AuthenticationToken = token;
			Thread.Sleep(TimeSpan.FromSeconds(30));
			address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeUri.Management.Api.Storages, UriParameter.Local);

			// Act.
			var storage = client.Get<Storage>(address);

			// Assert.
			Assert.NotNull(storage);
		}

		[Fact, Trait("Category", TestAssembly.Category)]
		public async Task Infrastructure_Get_Storage_Delayed_Admin_System()
		{
			// Arrange.
			var context = _testContext.HostTestContext;
			var credentials = new NetworkCredential(context.SystemAccountName, context.SystemAccountPassword);
			var addressFactory = new AddressFactory();
			var address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeUri.Authenticate);
			var client = _testContext.HostTestContext.CreateRestInfrastructureClient();
			var token = await client.Get<string>(address, credentials);
			Assert.True(!string.IsNullOrWhiteSpace(token));
			client.AuthenticationToken = token;
			Thread.Sleep(TimeSpan.FromSeconds(30));
			address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeUri.Management.Api.Storages, UriParameter.Local);

			// Act.
			var storage = client.Get<Storage>(address);

			// Assert.
			Assert.NotNull(storage);
		}

		[Fact, Trait("Category", TestAssembly.Category)]
        public void Infrastructure_Get_Storage_Delayed_Without_Authentication()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
	        var addressFactory = new AddressFactory();
            var address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeUri.Management.Api.Storages, UriParameter.Local);
	        var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

			// Act.
			var act = new Func<Task>(async () =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(30));
                var storage = await client.Get<Storage>(address);
                Assert.NotNull(storage);
            });

            // Assert.
            Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }
    }
}
