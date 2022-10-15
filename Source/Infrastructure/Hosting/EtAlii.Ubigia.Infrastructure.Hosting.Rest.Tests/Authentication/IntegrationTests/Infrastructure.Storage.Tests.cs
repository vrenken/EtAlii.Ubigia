// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Net;
#if UBIGIA_IS_RUNNING_ON_BUILD_AGENT == true // No need to run these slow tests on the local machine constantly.
	using System.Threading;
#endif
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Api.Transport;
	using EtAlii.Ubigia.Api.Transport.Rest;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
	public class InfrastructureStorageTests : IClassFixture<InfrastructureUnitTestContext>
	{
#if UBIGIA_IS_RUNNING_ON_BUILD_AGENT == true // No need to run these slow tests on the local machine constantly.
        private readonly TimeSpan _delay = TimeSpan.FromSeconds(30);
#endif

	    private readonly InfrastructureUnitTestContext _testContext;

        public InfrastructureStorageTests(InfrastructureUnitTestContext testContext)
        {
	        _testContext = testContext;
        }

		[Fact]
		public async Task Infrastructure_Get_Storage_Local_Admin_TestUser_With_Authentication()
		{
			// Arrange.
			var context = _testContext.Host;
			var credentials = new NetworkCredential(context.TestAccountName, context.TestAccountPassword);
			var addressFactory = new AddressFactory();
			var address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeUri.Authenticate);
			var client = _testContext.Host.CreateRestInfrastructureClient();
			var token = await client.Get<string>(address, credentials).ConfigureAwait(false);
			Assert.True(!string.IsNullOrWhiteSpace(token));
			client.AuthenticationToken = token;
			address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeManagementUri.Storages, UriParameter.Local);

			// Act.
			var storage = client.Get<Storage>(address);

			// Assert.
			Assert.NotNull(storage);
		}

		[Fact]
		public async Task Infrastructure_Get_Storage_Local_Admin_Admin_With_Authentication()
		{
			// Arrange.
			var context = _testContext.Host;
			var credentials = new NetworkCredential(context.AdminAccountName, context.AdminAccountPassword);
			var addressFactory = new AddressFactory();
			var address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeUri.Authenticate);
			var client = _testContext.Host.CreateRestInfrastructureClient();
			var token = await client.Get<string>(address, credentials).ConfigureAwait(false);
			Assert.True(!string.IsNullOrWhiteSpace(token));
			client.AuthenticationToken = token;
			address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeManagementUri.Storages, UriParameter.Local);

			// Act.
			var storage = client.Get<Storage>(address);

			// Assert.
			Assert.NotNull(storage);
		}

		[Fact]
		public async Task Infrastructure_Get_Storage_Local_Admin_System_With_Authentication()
		{
			// Arrange.
			var context = _testContext.Host;
			var credentials = new NetworkCredential(context.SystemAccountName, context.SystemAccountPassword);
			var addressFactory = new AddressFactory();
			var address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeUri.Authenticate);
			var client = _testContext.Host.CreateRestInfrastructureClient();
			var token = await client.Get<string>(address, credentials).ConfigureAwait(false);
			Assert.True(!string.IsNullOrWhiteSpace(token));
			client.AuthenticationToken = token;
			address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeManagementUri.Storages, UriParameter.Local);

			// Act.
			var storage = client.Get<Storage>(address);

			// Assert.
			Assert.NotNull(storage);
		}

		[Fact]
		public async Task Infrastructure_Get_Storage_Local_Without_Authentication()
		{
			// Arrange.
			var context = _testContext.Host;
			var addressFactory = new AddressFactory();
			var address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeManagementUri.Storages, UriParameter.Local);
			var client = _testContext.Host.CreateRestInfrastructureClient();

			// Act.
			var act = new Func<Task>(async () => await client.Get<Storage>(address).ConfigureAwait(false));

			// Assert.
			await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
		}

		[Fact]
		public async Task Infrastructure_Get_Accounts_Without_Authentication()
		{
			// Arrange.
			var context = _testContext.Host;
			var addressFactory = new AddressFactory();
			var address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeManagementUri.Accounts);
			var client = _testContext.Host.CreateRestInfrastructureClient();

			// Act.
			var act = new Func<Task>(async () => await client.Get<IEnumerable<Account>>(address).ConfigureAwait(false));

			// Assert.
			await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
		}

#if UBIGIA_IS_RUNNING_ON_BUILD_AGENT == true // No need to run these slow tests on the local machine constantly.

		[Fact]
		public async Task Infrastructure_Get_Storage_Delayed_Admin_TestUser()
		{
			// Arrange.
			var context = _testContext.Host;
			var credentials = new NetworkCredential(context.TestAccountName, context.TestAccountPassword);
			var addressFactory = new AddressFactory();
			var address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeUri.Authenticate);
			var client = _testContext.Host.CreateRestInfrastructureClient();
			var token = await client.Get<string>(address, credentials).ConfigureAwait(false);
			Assert.True(!string.IsNullOrWhiteSpace(token));
			client.AuthenticationToken = token;
            await Task.Delay(_delay).ConfigureAwait(false);
			address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeManagementUri.Storages, UriParameter.Local);

			// Act.
			var storage = client.Get<Storage>(address);

			// Assert.
			Assert.NotNull(storage);
		}

		[Fact]
		public async Task Infrastructure_Get_Storage_Delayed_Admin_Admin()
		{
			// Arrange.
			var context = _testContext.Host;
			var credentials = new NetworkCredential(context.AdminAccountName, context.AdminAccountPassword);
			var addressFactory = new AddressFactory();
			var address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeUri.Authenticate);
			var client = _testContext.Host.CreateRestInfrastructureClient();
			var token = await client.Get<string>(address, credentials).ConfigureAwait(false);
			Assert.True(!string.IsNullOrWhiteSpace(token));
			client.AuthenticationToken = token;
			await Task.Delay(_delay).ConfigureAwait(false);
			address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeManagementUri.Storages, UriParameter.Local);

			// Act.
			var storage = client.Get<Storage>(address);

			// Assert.
			Assert.NotNull(storage);
		}

		[Fact]
		public async Task Infrastructure_Get_Storage_Delayed_Admin_System()
		{
			// Arrange.
			var context = _testContext.Host;
			var credentials = new NetworkCredential(context.SystemAccountName, context.SystemAccountPassword);
			var addressFactory = new AddressFactory();
			var address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeUri.Authenticate);
			var client = _testContext.Host.CreateRestInfrastructureClient();
			var token = await client.Get<string>(address, credentials).ConfigureAwait(false);
			Assert.True(!string.IsNullOrWhiteSpace(token));
			client.AuthenticationToken = token;
            await Task.Delay(_delay).ConfigureAwait(false);
			address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeManagementUri.Storages, UriParameter.Local);

			// Act.
			var storage = client.Get<Storage>(address);

			// Assert.
			Assert.NotNull(storage);
		}

		[Fact]
        public void Infrastructure_Get_Storage_Delayed_Without_Authentication()
        {
			// Arrange.
	        var context = _testContext.Host;
	        var addressFactory = new AddressFactory();
            var address = addressFactory.Create(context.ServiceDetails.ManagementAddress, RelativeManagementUri.Storages, UriParameter.Local);
	        var client = _testContext.Host.CreateRestInfrastructureClient();

			// Act.
			var act = new Func<Task>(async () =>
            {
                Thread.Sleep(_delay);
                var storage = await client.Get<Storage>(address).ConfigureAwait(false);
                Assert.NotNull(storage);
            });

            // Assert.
            Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }
#endif
    }
}
