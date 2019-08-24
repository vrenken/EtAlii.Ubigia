﻿namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
	using System;
	using System.Net;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Api.Transport;
	using EtAlii.Ubigia.Api.Transport.WebApi;
	using Xunit;
	using RelativeUri = EtAlii.Ubigia.Infrastructure.Transport.NetCore.RelativeUri;

	[Trait("Technology", "NetCore")]
	public class InfrastructureAuthenticationTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;

        public InfrastructureAuthenticationTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_TestUser()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var credentials = new NetworkCredential(context.TestAccountName, context.TestAccountPassword);
		    var addressFactory = new AddressFactory();
		    var address = addressFactory.Create(context.DataServiceAddress, RelativeUri.Authenticate);
		    var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

		    // Act.
		    var token = await client.Get<string>(address, credentials);

		    // Assert.
		    Assert.True(!string.IsNullOrWhiteSpace(token));
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_System()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var credentials = new NetworkCredential(context.SystemAccountName, context.SystemAccountPassword);
		    var addressFactory = new AddressFactory();
		    var address = addressFactory.Create(context.DataServiceAddress, RelativeUri.Authenticate);
		    var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

		    // Act.
		    var token = await client.Get<string>(address, credentials);

		    // Assert.
		    Assert.True(!string.IsNullOrWhiteSpace(token));
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_Admin()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var credentials = new NetworkCredential(context.AdminAccountName, context.AdminAccountPassword);
		    var addressFactory = new AddressFactory();
		    var address = addressFactory.Create(context.DataServiceAddress, RelativeUri.Authenticate);
		    var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

		    // Act.
		    var token = await client.Get<string>(address, credentials);

		    // Assert.
		    Assert.True(!string.IsNullOrWhiteSpace(token));
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_Admin_TestUser()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var credentials = new NetworkCredential(context.TestAccountName, context.TestAccountPassword);
		    var addressFactory = new AddressFactory();
		    var address = addressFactory.Create(context.ManagementServiceAddress, RelativeUri.Authenticate);
		    var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

		    // Act.
		    var token = await client.Get<string>(address, credentials);

		    // Assert.
		    Assert.True(!string.IsNullOrWhiteSpace(token));
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_Admin_System()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var credentials = new NetworkCredential(context.SystemAccountName, context.SystemAccountPassword);
		    var addressFactory = new AddressFactory();
		    var address = addressFactory.Create(context.ManagementServiceAddress, RelativeUri.Authenticate);
		    var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

		    // Act.
		    var token = await client.Get<string>(address, credentials);

		    // Assert.
		    Assert.True(!string.IsNullOrWhiteSpace(token));
	    }

		[Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_Admin_Admin()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var credentials = new NetworkCredential(context.AdminAccountName, context.AdminAccountPassword);
		    var addressFactory = new AddressFactory();
		    var address = addressFactory.Create(context.ManagementServiceAddress, RelativeUri.Authenticate);
		    var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

		    // Act.
		    var token = await client.Get<string>(address, credentials);

		    // Assert.
		    Assert.True(!string.IsNullOrWhiteSpace(token));
	    }

		[Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_TestUser_Invalid_Password()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var credentials = new NetworkCredential(context.TestAccountName, context.TestAccountPassword + "BAAD");
		    var addressFactory = new AddressFactory();
		    var address = addressFactory.Create(context.DataServiceAddress, RelativeUri.Authenticate);
		    var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

		    // Act
		    var act = new Func<Task>(async () => await client.Get<string>(address, credentials));

		    // Assert.
		    await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_System_Invalid_Password()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var credentials = new NetworkCredential(context.SystemAccountName, context.SystemAccountPassword + "BAAD");
		    var addressFactory = new AddressFactory();
		    var address = addressFactory.Create(context.DataServiceAddress, RelativeUri.Authenticate);
		    var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

		    // Act
		    var act = new Func<Task>(async () => await client.Get<string>(address, credentials));

		    // Assert.
		    await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_Admin_Invalid_Password()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var credentials = new NetworkCredential(context.AdminAccountName, context.AdminAccountPassword + "BAAD");
		    var addressFactory = new AddressFactory();
		    var address = addressFactory.Create(context.DataServiceAddress, RelativeUri.Authenticate);
		    var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

		    // Act
		    var act = new Func<Task>(async () => await client.Get<string>(address, credentials));

		    // Assert.
		    await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_Admin_System_Invalid_Password()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var credentials = new NetworkCredential(context.SystemAccountName, context.SystemAccountPassword + "BAAD");
		    var addressFactory = new AddressFactory();
		    var address = addressFactory.Create(context.ManagementServiceAddress, RelativeUri.Authenticate);
		    var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

		    // Act
		    var act = new Func<Task>(async () => await client.Get<string>(address, credentials));

		    // Assert.
		    await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
	    }
	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_Admin_Admin_Invalid_Password()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var credentials = new NetworkCredential(context.AdminAccountName, context.AdminAccountPassword + "BAAD");
		    var addressFactory = new AddressFactory();
		    var address = addressFactory.Create(context.ManagementServiceAddress, RelativeUri.Authenticate);
		    var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

		    // Act
		    var act = new Func<Task>(async () => await client.Get<string>(address, credentials));

		    // Assert.
		    await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
	    }

		[Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_TestUser_Invalid_Account()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var credentials = new NetworkCredential(context.TestAccountName + "BAAD", context.TestAccountPassword);
		    var addressFactory = new AddressFactory();
		    var address = addressFactory.Create(context.DataServiceAddress, RelativeUri.Authenticate);
		    var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

		    // Act
		    var act = new Func<Task>(async () => await client.Get<string>(address, credentials));

		    // Assert.
		    await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_System_Invalid_Account()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var credentials = new NetworkCredential(context.SystemAccountName + "BAAD", context.SystemAccountPassword);
		    var addressFactory = new AddressFactory();
		    var address = addressFactory.Create(context.DataServiceAddress, RelativeUri.Authenticate);
		    var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

		    // Act
		    var act = new Func<Task>(async () => await client.Get<string>(address, credentials));

		    // Assert.
		    await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_Admin_Invalid_Account()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var credentials = new NetworkCredential(context.AdminAccountName + "BAAD", context.AdminAccountPassword);
		    var addressFactory = new AddressFactory();
		    var address = addressFactory.Create(context.DataServiceAddress, RelativeUri.Authenticate);
		    var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

		    // Act
		    var act = new Func<Task>(async () => await client.Get<string>(address, credentials));

		    // Assert.
		    await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_Admin_System_Invalid_Account()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var credentials = new NetworkCredential(context.SystemAccountName + "BAAD", context.SystemAccountPassword);
		    var addressFactory = new AddressFactory();
		    var address = addressFactory.Create(context.ManagementServiceAddress, RelativeUri.Authenticate);
		    var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

		    // Act
		    var act = new Func<Task>(async () => await client.Get<string>(address, credentials));

		    // Assert.
		    await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_Admin_Admin_Invalid_Account()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var credentials = new NetworkCredential(context.AdminAccountName + "BAAD", context.AdminAccountPassword);
		    var addressFactory = new AddressFactory();
		    var address = addressFactory.Create(context.ManagementServiceAddress, RelativeUri.Authenticate);
		    var client = _testContext.HostTestContext.CreateRestInfrastructureClient();

		    // Act
		    var act = new Func<Task>(async () => await client.Get<string>(address, credentials));

		    // Assert.
		    await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
	    }
	}
}
