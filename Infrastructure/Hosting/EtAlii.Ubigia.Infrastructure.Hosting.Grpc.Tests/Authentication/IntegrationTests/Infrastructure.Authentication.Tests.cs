namespace EtAlii.Ubigia.Infrastructure.Hosting.Grpc.Tests
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Api.Transport.Grpc;
	using global::Grpc.Core;
	using Xunit;
	using UserAuthenticationClient = EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationGrpcService.AuthenticationGrpcServiceClient;
    using UserAuthenticationRequest = EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest;
    using AdminAuthenticationClient = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AuthenticationGrpcService.AuthenticationGrpcServiceClient;
    using AdminAuthenticationRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AuthenticationRequest;

    [Trait("Technology", "Grpc")]
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
		    var channel = context.CreateUserGrpcInfrastructureChannel();
		    var client = new UserAuthenticationClient(channel);
		    var request = new UserAuthenticationRequest { AccountName = context.TestAccountName, Password = context.TestAccountPassword, HostIdentifier = context.HostIdentifier };
	
		    // Act.
		    var call = client.AuthenticateAsync(request);
		    var response = await call.ResponseAsync;
		        
		    // Assert.
		    Assert.NotNull(response);
		    var authenticationToken = call
			    .GetTrailers()
			    .Single(header => header.Key == GrpcHeader.AuthenticationTokenHeaderKey).Value;
		    Assert.False(String.IsNullOrWhiteSpace(authenticationToken));
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_System()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = context.CreateUserGrpcInfrastructureChannel();
		    var client = new UserAuthenticationClient(channel);
		    var request = new UserAuthenticationRequest { AccountName = context.SystemAccountName, Password = context.SystemAccountPassword, HostIdentifier = context.HostIdentifier };
		    
		    // Act.
		    var call = client.AuthenticateAsync(request);
		    var response = await call.ResponseAsync;
		        
		    // Assert.
		    Assert.NotNull(response);
		    var authenticationToken = call
			    .GetTrailers()
			    .Single(header => header.Key == GrpcHeader.AuthenticationTokenHeaderKey).Value;
		    Assert.False(String.IsNullOrWhiteSpace(authenticationToken));
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_Admin()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = context.CreateUserGrpcInfrastructureChannel();
		    var client = new UserAuthenticationClient(channel);
		    var request = new UserAuthenticationRequest { AccountName = context.AdminAccountName, Password = context.AdminAccountPassword, HostIdentifier = context.HostIdentifier };

		    // Act.
		    var call = client.AuthenticateAsync(request);
		    var response = await call.ResponseAsync;
		        
		    // Assert.
		    Assert.NotNull(response);
		    var authenticationToken = call
			    .GetTrailers()
			    .Single(header => header.Key == GrpcHeader.AuthenticationTokenHeaderKey).Value;
		    Assert.False(String.IsNullOrWhiteSpace(authenticationToken));
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_Admin_TestUser()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = context.CreateAdminGrpcInfrastructureChannel();
		    var client = new AdminAuthenticationClient(channel);
		    var request = new AdminAuthenticationRequest { AccountName = context.TestAccountName, Password = context.TestAccountPassword, HostIdentifier = context.HostIdentifier };

		    // Act.
		    var call = client.AuthenticateAsync(request);
		    var response = await call.ResponseAsync;
		    
		    // Assert.
		    Assert.NotNull(response);
		    var authenticationToken = call
			    .GetTrailers()
			    .SingleOrDefault(trailer => trailer.Key == GrpcHeader.AuthenticationTokenHeaderKey)?.Key;
		    Assert.False(String.IsNullOrWhiteSpace(authenticationToken));
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_Admin_System()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = context.CreateAdminGrpcInfrastructureChannel();
		    var client = new AdminAuthenticationClient(channel);
		    var request = new AdminAuthenticationRequest { AccountName = context.SystemAccountName, Password = context.SystemAccountPassword, HostIdentifier = context.HostIdentifier };
		    
		    // Act.
		    var call = client.AuthenticateAsync(request);
		    var response = await call.ResponseAsync;
		    
		    // Assert.
		    Assert.NotNull(response);
		    var authenticationToken = call
			    .GetTrailers()
			    .SingleOrDefault(trailer => trailer.Key == GrpcHeader.AuthenticationTokenHeaderKey)?.Key;
		    Assert.False(String.IsNullOrWhiteSpace(authenticationToken));
	    }

		[Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_Admin_Admin()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = context.CreateAdminGrpcInfrastructureChannel();
		    var client = new AdminAuthenticationClient(channel);
		    var request = new AdminAuthenticationRequest { AccountName = context.AdminAccountName, Password = context.AdminAccountPassword, HostIdentifier = context.HostIdentifier };
		    
		    // Act.
		    var call = client.AuthenticateAsync(request);
		    var response = await call.ResponseAsync;

		    // Assert.
		    Assert.NotNull(response);
		    var authenticationToken = call
			    .GetTrailers()
			    .SingleOrDefault(trailer => trailer.Key == GrpcHeader.AuthenticationTokenHeaderKey)?.Key;
		    Assert.False(String.IsNullOrWhiteSpace(authenticationToken));
	    }

		[Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_TestUser_Invalid_Password()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = context.CreateUserGrpcInfrastructureChannel();
		    var client = new UserAuthenticationClient(channel);
		    var request = new UserAuthenticationRequest { AccountName = context.TestAccountName, Password = context.TestAccountPassword + "BAAD", HostIdentifier = context.HostIdentifier };

		    // Act
		    var act = new Func<Task>(async () => await client.AuthenticateAsync(request));

		    // Assert.
		    await Assert.ThrowsAsync<RpcException>(act); // UnauthorizedInfrastructureOperationException
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_System_Invalid_Password()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = context.CreateUserGrpcInfrastructureChannel();
		    var client = new UserAuthenticationClient(channel);
		    var request = new UserAuthenticationRequest { AccountName = context.SystemAccountName, Password = context.SystemAccountPassword + "BAAD", HostIdentifier = context.HostIdentifier };

		    // Act
		    var act = new Func<Task>(async () => await client.AuthenticateAsync(request));

		    // Assert.
		    await Assert.ThrowsAsync<RpcException>(act); // UnauthorizedInfrastructureOperationException
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_Admin_Invalid_Password()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = context.CreateUserGrpcInfrastructureChannel();
		    var client = new UserAuthenticationClient(channel);
		    var request = new UserAuthenticationRequest { AccountName = context.AdminAccountName, Password = context.AdminAccountPassword + "BAAD", HostIdentifier = context.HostIdentifier };

		    // Act
		    var act = new Func<Task>(async () => await client.AuthenticateAsync(request));

		    // Assert.
		    await Assert.ThrowsAsync<RpcException>(act); // UnauthorizedInfrastructureOperationException
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_Admin_System_Invalid_Password()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = context.CreateAdminGrpcInfrastructureChannel();
		    var client = new AdminAuthenticationClient(channel);
		    var request = new AdminAuthenticationRequest { AccountName = context.SystemAccountName, Password = context.SystemAccountPassword + "BAAD", HostIdentifier = context.HostIdentifier };

		    // Act
		    var act = new Func<Task>(async () => await client.AuthenticateAsync(request));

		    // Assert.
		    await Assert.ThrowsAsync<RpcException>(act); // UnauthorizedInfrastructureOperationException
	    }
	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_Admin_Admin_Invalid_Password()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = context.CreateAdminGrpcInfrastructureChannel();
		    var client = new AdminAuthenticationClient(channel);
		    var request = new AdminAuthenticationRequest { AccountName = context.AdminAccountName, Password = context.AdminAccountPassword + "BAAD", HostIdentifier = context.HostIdentifier };

		    // Act
		    var act = new Func<Task>(async () => await client.AuthenticateAsync(request));

		    // Assert.
		    await Assert.ThrowsAsync<RpcException>(act); // UnauthorizedInfrastructureOperationException
	    }

		[Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_TestUser_Invalid_Account()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = context.CreateUserGrpcInfrastructureChannel();
		    var client = new UserAuthenticationClient(channel);
		    var request = new UserAuthenticationRequest { AccountName = context.TestAccountName + "BAAD", Password = context.TestAccountPassword, HostIdentifier = context.HostIdentifier };

		    // Act
		    var act = new Func<Task>(async () => await client.AuthenticateAsync(request));

		    // Assert.
		    await Assert.ThrowsAsync<RpcException>(act); // UnauthorizedInfrastructureOperationException
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_System_Invalid_Account()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = context.CreateUserGrpcInfrastructureChannel();
		    var client = new UserAuthenticationClient(channel);
		    var request = new UserAuthenticationRequest { AccountName = context.SystemAccountName + "BAAD", Password = context.SystemAccountPassword, HostIdentifier = context.HostIdentifier };

		    // Act
		    var act = new Func<Task>(async () => await client.AuthenticateAsync(request));

		    // Assert.
		    await Assert.ThrowsAsync<RpcException>(act); // UnauthorizedInfrastructureOperationException
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_User_Admin_Invalid_Account()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = context.CreateUserGrpcInfrastructureChannel();
		    var client = new UserAuthenticationClient(channel);
		    var request = new UserAuthenticationRequest { AccountName = context.AdminAccountName + "BAAD", Password = context.AdminAccountPassword, HostIdentifier = context.HostIdentifier };

		    // Act
		    var act = new Func<Task>(async () => await client.AuthenticateAsync(request));

		    // Assert.
		    await Assert.ThrowsAsync<RpcException>(act); // UnauthorizedInfrastructureOperationException
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_Admin_System_Invalid_Account()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = context.CreateAdminGrpcInfrastructureChannel();
		    var client = new AdminAuthenticationClient(channel);
		    var request = new AdminAuthenticationRequest { AccountName = context.SystemAccountName + "BAAD", Password = context.SystemAccountPassword, HostIdentifier = context.HostIdentifier };

		    // Act
		    var act = new Func<Task>(async () => await client.AuthenticateAsync(request));

		    // Assert.
		    await Assert.ThrowsAsync<RpcException>(act); // UnauthorizedInfrastructureOperationException
	    }

	    [Fact, Trait("Category", TestAssembly.Category)]
	    public async Task Infrastructure_Get_Authentication_Url_Admin_Admin_Invalid_Account()
	    {
		    // Arrange.
		    var context = _testContext.HostTestContext;
		    var channel = context.CreateAdminGrpcInfrastructureChannel();
		    var client = new AdminAuthenticationClient(channel);
		    var request = new AdminAuthenticationRequest { AccountName = context.AdminAccountName + "BAAD", Password = context.AdminAccountPassword, HostIdentifier = context.HostIdentifier };

		    // Act
		    var act = new Func<Task>(async () => await client.AuthenticateAsync(request));

		    // Assert.
		    await Assert.ThrowsAsync<RpcException>(act); // UnauthorizedInfrastructureOperationException
	    }
	}
}
