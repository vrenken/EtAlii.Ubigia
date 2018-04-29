namespace EtAlii.Ubigia.Infrastructure.Hosting.Grpc.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using UserAuthenticationClient = EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationGrpcService.AuthenticationGrpcServiceClient;
    using AdminAuthenticationClient = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AuthenticationGrpcService.AuthenticationGrpcServiceClient;
    using UserAuthenticationRequest = EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest;
    using AdminAuthenticationRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AuthenticationRequest;
    using Xunit;

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
		    var channel = _testContext.HostTestContext.CreateGrpcInfrastructureChannel();
		    var client = new UserAuthenticationClient(channel);
		    var request = new UserAuthenticationRequest { AccountName = context.TestAccountName, Password = context.TestAccountPassword };
			
		    // Act.
		    var response = await client.AuthenticateAsync(request);

		    // Assert.
		    Assert.NotNull(response);
		    Assert.False(String.IsNullOrWhiteSpace(response.AuthenticationToken));
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_System()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = _testContext.HostTestContext.CreateGrpcInfrastructureChannel();
		    var client = new UserAuthenticationClient(channel);
		    var request = new UserAuthenticationRequest { AccountName = context.SystemAccountName, Password = context.SystemAccountPassword};

		    // Act.
		    var response = await client.AuthenticateAsync(request);

		    // Assert.
		    Assert.NotNull(response);
		    Assert.False(String.IsNullOrWhiteSpace(response.AuthenticationToken));
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_Admin()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = _testContext.HostTestContext.CreateGrpcInfrastructureChannel();
		    var client = new UserAuthenticationClient(channel);
		    var request = new UserAuthenticationRequest { AccountName = context.AdminAccountName, Password = context.AdminAccountPassword};
		    
		    // Act.
		    var response = await client.AuthenticateAsync(request);

		    // Assert.
		    Assert.NotNull(response);
		    Assert.False(String.IsNullOrWhiteSpace(response.AuthenticationToken));
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_Admin_TestUser()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = _testContext.HostTestContext.CreateGrpcInfrastructureChannel();
		    var client = new AdminAuthenticationClient(channel);
		    var request = new AdminAuthenticationRequest { AccountName = context.TestAccountName, Password = context.TestAccountPassword};

		    // Act.
		    var response = await client.AuthenticateAsync(request);

		    // Assert.
		    Assert.NotNull(response);
		    Assert.False(String.IsNullOrWhiteSpace(response.AuthenticationToken));
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_Admin_System()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = _testContext.HostTestContext.CreateGrpcInfrastructureChannel();
		    var client = new AdminAuthenticationClient(channel);
		    var request = new AdminAuthenticationRequest { AccountName = context.SystemAccountName, Password = context.SystemAccountPassword};
		    
		    // Act.
		    var response = await client.AuthenticateAsync(request);

		    // Assert.
		    Assert.NotNull(response);
		    Assert.False(String.IsNullOrWhiteSpace(response.AuthenticationToken));
	    }

		[Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_Admin_Admin()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = _testContext.HostTestContext.CreateGrpcInfrastructureChannel();
		    var client = new AdminAuthenticationClient(channel);
		    var request = new AdminAuthenticationRequest { AccountName = context.AdminAccountName, Password = context.AdminAccountPassword};
		    
		    // Act.
		    var response = await client.AuthenticateAsync(request);

		    // Assert.
		    Assert.NotNull(response);
		    Assert.False(String.IsNullOrWhiteSpace(response.AuthenticationToken));
	    }

		[Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_TestUser_Invalid_Password()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = _testContext.HostTestContext.CreateGrpcInfrastructureChannel();
		    var client = new UserAuthenticationClient(channel);
		    var request = new UserAuthenticationRequest { AccountName = context.TestAccountName, Password = context.TestAccountPassword + "BAAD"};

		    // Act
		    var act = new Func<Task>(async () => await client.AuthenticateAsync(request));

		    // Assert.
		    await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_System_Invalid_Password()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = _testContext.HostTestContext.CreateGrpcInfrastructureChannel();
		    var client = new UserAuthenticationClient(channel);
		    var request = new UserAuthenticationRequest { AccountName = context.SystemAccountName, Password = context.SystemAccountPassword + "BAAD"};

		    // Act
		    var act = new Func<Task>(async () => await client.AuthenticateAsync(request));

		    // Assert.
		    await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_Admin_Invalid_Password()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = _testContext.HostTestContext.CreateGrpcInfrastructureChannel();
		    var client = new UserAuthenticationClient(channel);
		    var request = new UserAuthenticationRequest { AccountName = context.AdminAccountName, Password = context.AdminAccountPassword + "BAAD"};

		    // Act
		    var act = new Func<Task>(async () => await client.AuthenticateAsync(request));

		    // Assert.
		    await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_Admin_System_Invalid_Password()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = _testContext.HostTestContext.CreateGrpcInfrastructureChannel();
		    var client = new AdminAuthenticationClient(channel);
		    var request = new AdminAuthenticationRequest { AccountName = context.SystemAccountName, Password = context.SystemAccountPassword + "BAAD"};

		    // Act
		    var act = new Func<Task>(async () => await client.AuthenticateAsync(request));

		    // Assert.
		    await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
	    }
	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_Admin_Admin_Invalid_Password()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = _testContext.HostTestContext.CreateGrpcInfrastructureChannel();
		    var client = new AdminAuthenticationClient(channel);
		    var request = new AdminAuthenticationRequest { AccountName = context.AdminAccountName, Password = context.AdminAccountPassword + "BAAD"};

		    // Act
		    var act = new Func<Task>(async () => await client.AuthenticateAsync(request));

		    // Assert.
		    await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
	    }

		[Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_TestUser_Invalid_Account()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = _testContext.HostTestContext.CreateGrpcInfrastructureChannel();
		    var client = new UserAuthenticationClient(channel);
		    var request = new UserAuthenticationRequest { AccountName = context.TestAccountName + "BAAD", Password = context.TestAccountPassword};

		    // Act
		    var act = new Func<Task>(async () => await client.AuthenticateAsync(request));

		    // Assert.
		    await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_System_Invalid_Account()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = _testContext.HostTestContext.CreateGrpcInfrastructureChannel();
		    var client = new UserAuthenticationClient(channel);
		    var request = new UserAuthenticationRequest { AccountName = context.SystemAccountName + "BAAD", Password = context.SystemAccountPassword};

		    // Act
		    var act = new Func<Task>(async () => await client.AuthenticateAsync(request));

		    // Assert.
		    await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_Admin_Invalid_Account()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = _testContext.HostTestContext.CreateGrpcInfrastructureChannel();
		    var client = new UserAuthenticationClient(channel);
		    var request = new UserAuthenticationRequest { AccountName = context.AdminAccountName + "BAAD", Password = context.AdminAccountPassword};

		    // Act
		    var act = new Func<Task>(async () => await client.AuthenticateAsync(request));

		    // Assert.
		    await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_Admin_System_Invalid_Account()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = _testContext.HostTestContext.CreateGrpcInfrastructureChannel();
		    var client = new AdminAuthenticationClient(channel);
		    var request = new AdminAuthenticationRequest { AccountName = context.SystemAccountName + "BAAD", Password = context.SystemAccountPassword};

		    // Act
		    var act = new Func<Task>(async () => await client.AuthenticateAsync(request));

		    // Assert.
		    await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_Admin_Admin_Invalid_Account()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = _testContext.HostTestContext.CreateGrpcInfrastructureChannel();
		    var client = new AdminAuthenticationClient(channel);
		    var request = new AdminAuthenticationRequest { AccountName = context.AdminAccountName + "BAAD", Password = context.AdminAccountPassword};

		    // Act
		    var act = new Func<Task>(async () => await client.AuthenticateAsync(request));

		    // Assert.
		    await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
	    }
	}
}
