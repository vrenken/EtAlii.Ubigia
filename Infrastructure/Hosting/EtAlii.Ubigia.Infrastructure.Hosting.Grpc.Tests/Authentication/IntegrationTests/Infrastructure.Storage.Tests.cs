﻿namespace EtAlii.Ubigia.Infrastructure.Hosting.Grpc.Tests
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc;
    using global::Grpc.Core;
    using UserAuthenticationClient = EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationGrpcService.AuthenticationGrpcServiceClient;
    using UserAuthenticationRequest = EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest;
    using AdminAuthenticationClient = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AuthenticationGrpcService.AuthenticationGrpcServiceClient;
    using AdminAuthenticationRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AuthenticationRequest;
    using AdminStorageClient = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageGrpcService.StorageGrpcServiceClient;
    using AdminStorageRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageSingleRequest;
    using Xunit;
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;

	[Trait("Technology", "Grpc")]
	public class InfrastructureStorageTests : IClassFixture<InfrastructureUnitTestContext>
	{
	    private readonly InfrastructureUnitTestContext _testContext;

        public InfrastructureStorageTests(InfrastructureUnitTestContext testContext)
        {
	        _testContext = testContext;
        }

		private async Task<Metadata> CreateAuthenticationHeaders(Channel channel, InProcessInfrastructureHostTestContext context)
		{
			var authenticationClient = new AdminAuthenticationClient(channel);
			var authenticationRequest = new AdminAuthenticationRequest { AccountName = context.TestAccountName, Password = context.TestAccountPassword, HostIdentifier = context.HostIdentifier };
			
			var call = authenticationClient.AuthenticateAsync(authenticationRequest);
			var authenticationResponse = await call.ResponseAsync; 
			var authenticationToken = call
				.GetTrailers()
				.SingleOrDefault(trailer => trailer.Key == GrpcHeader.AuthenticationTokenHeaderKey)?.Value;
			var headers = new Metadata {{GrpcHeader.AuthenticationTokenHeaderKey, authenticationToken}};
			return headers;
		}
		
		[Fact, Trait("Category", TestAssembly.Category)]
		public async Task Infrastructure_Get_Storage_Local_Admin_TestUser_With_Authentication()
		{
			// Arrange.
			var context = _testContext.HostTestContext;
			var channel = context.CreateAdminGrpcInfrastructureChannel();
			var headers = await CreateAuthenticationHeaders(channel, context);
			var client = new AdminStorageClient(channel);
			var request = new AdminStorageRequest();
			
			// Act.
			var response = await client.GetLocalAsync(request, headers);
		
			// Assert.
			Assert.NotNull(response);
			Assert.NotNull(response.Storage);
			Assert.NotNull(response.Storage.Id);
			Assert.NotEqual(Guid.Empty, response.Storage.Id.ToLocal());
		}

		[Fact, Trait("Category", TestAssembly.Category)]
		public async Task Infrastructure_Get_Storage_Local_Admin_Admin_With_Authentication()
		{
			// Arrange.
			var context = _testContext.HostTestContext;
			var channel = context.CreateAdminGrpcInfrastructureChannel();
			var headers = await CreateAuthenticationHeaders(channel, context);
			var client = new AdminStorageClient(channel);
			var request = new AdminStorageRequest();

			// Act.
			var response = await client.GetLocalAsync(request, headers);
		
			// Assert.
			Assert.NotNull(response);
			Assert.NotNull(response.Storage);
			Assert.NotNull(response.Storage.Id);
			Assert.NotEqual(Guid.Empty, response.Storage.Id.ToLocal());
		}

		[Fact, Trait("Category", TestAssembly.Category)]
		public async Task Infrastructure_Get_Storage_Local_Admin_System_With_Authentication()
		{
			// Arrange.
			var context = _testContext.HostTestContext;
			var channel = context.CreateAdminGrpcInfrastructureChannel();
			var headers = await CreateAuthenticationHeaders(channel, context);
			var client = new AdminStorageClient(channel);
			var request = new AdminStorageRequest();

			// Act.
			var response = await client.GetLocalAsync(request, headers);
		
			// Assert.
			Assert.NotNull(response);
			Assert.NotNull(response.Storage);
			Assert.NotNull(response.Storage.Id);
			Assert.NotEqual(Guid.Empty, response.Storage.Id.ToLocal());
		}
		
		[Fact, Trait("Category", TestAssembly.Category)]
        public async Task Infrastructure_Get_Storage_Local_Without_Authentication()
        {
			// Arrange.
	        var channel = _testContext.HostTestContext.CreateAdminGrpcInfrastructureChannel();
	        var client = new AdminStorageClient(channel);
	        var request = new AdminStorageRequest();
	        
			// Act.
			var act = new Func<Task>(async () => await client.GetLocalAsync(request));

            // Assert.
            await Assert.ThrowsAsync<RpcException>(act); // InvalidInfrastructureOperationException
        }

		[Fact(Skip = "Not working (yet)"), Trait("Category", TestAssembly.Category)]
		public async Task Infrastructure_Get_Storage_Delayed_Admin_TestUser()
		{
			// Arrange.
			var context = _testContext.HostTestContext;
			var channel = context.CreateAdminGrpcInfrastructureChannel();
			var headers = await CreateAuthenticationHeaders(channel, context);
			Thread.Sleep(50000);
			var client = new AdminStorageClient(channel);
			var request = new AdminStorageRequest();

			// Act.
			var response = await client.GetLocalAsync(request, headers);

			// Assert.
			Assert.NotNull(response);
			Assert.NotNull(response.Storage);
			Assert.NotNull(response.Storage.Id);
			Assert.NotEqual(Guid.Empty, response.Storage.Id.ToLocal());
		}

		[Fact(Skip = "Not working (yet)"), Trait("Category", TestAssembly.Category)]
		public async Task Infrastructure_Get_Storage_Delayed_Admin_Admin()
		{
			// Arrange.
			var context = _testContext.HostTestContext;
			var channel = context.CreateAdminGrpcInfrastructureChannel();
			var headers = await CreateAuthenticationHeaders(channel, context);
			Thread.Sleep(50000);
			var client = new AdminStorageClient(channel);
			var request = new AdminStorageRequest();

			// Act.
			var response = await client.GetLocalAsync(request, headers);

			// Assert.
			Assert.NotNull(response);
			Assert.NotNull(response.Storage);
			Assert.NotNull(response.Storage.Id);
			Assert.NotEqual(Guid.Empty, response.Storage.Id.ToLocal());
		}

		[Fact(Skip = "Not working (yet)"), Trait("Category", TestAssembly.Category)]
		public async Task Infrastructure_Get_Storage_Delayed_Admin_System()
		{
			// Arrange.
			var context = _testContext.HostTestContext;
			var channel = context.CreateAdminGrpcInfrastructureChannel();
			var headers = await CreateAuthenticationHeaders(channel, context);
			Thread.Sleep(50000);
			var client = new AdminStorageClient(channel);
			var request = new AdminStorageRequest();
			
			// Act.
			var response = await client.GetLocalAsync(request, headers);

			// Assert.
			Assert.NotNull(response);
			Assert.NotNull(response.Storage);
			Assert.NotNull(response.Storage.Id);
			Assert.NotEqual(Guid.Empty, response.Storage.Id.ToLocal());
		}

		[Fact(Skip = "Not working (yet)"), Trait("Category", TestAssembly.Category)]
		public async void Infrastructure_Get_Storage_Delayed_Without_Authentication_01()
		{
			// Arrange.
			var channel = _testContext.HostTestContext.CreateAdminGrpcInfrastructureChannel();
			var client = new AdminStorageClient(channel);
			var request = new AdminStorageRequest();
			Thread.Sleep(50000);
	        
			// Act.
			var act = new Func<Task>(async () => await client.GetLocalAsync(request));

			// Assert.
			var exception = await Assert.ThrowsAsync<RpcException>(act); // InvalidInfrastructureOperationException
		}
		
		[Fact(Skip = "Not working (yet)"), Trait("Category", TestAssembly.Category)]
		public async void Infrastructure_Get_Storage_Delayed_Without_Authentication_02()
		{
			// Arrange.
			var channel = _testContext.HostTestContext.CreateAdminGrpcInfrastructureChannel();
			Thread.Sleep(50000);
			var client = new AdminStorageClient(channel);
			var request = new AdminStorageRequest();

			// Act.
			var act = new Func<Task>(async () => await client.GetLocalAsync(request));

			// Assert.
			await Assert.ThrowsAsync<RpcException>(act); // InvalidInfrastructureOperationException
		}
    }
}
