// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
	using System;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Api.Transport.Grpc;
	using EtAlii.Ubigia.Api.Transport.Management.Grpc;
	using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
	using Grpc.Core;
	using Grpc.Net.Client;
	using Xunit;
	using AdminAuthenticationClient = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AuthenticationGrpcService.AuthenticationGrpcServiceClient;
    using AdminAuthenticationRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AuthenticationRequest;
    using AdminStorageClient = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageGrpcService.StorageGrpcServiceClient;
    using AdminStorageRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageSingleRequest;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
	public class InfrastructureStorageTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly TimeSpan _delay = TimeSpan.FromMilliseconds(50000);

	    private readonly InfrastructureUnitTestContext _testContext;

        public InfrastructureStorageTests(InfrastructureUnitTestContext testContext)
        {
	        _testContext = testContext;
        }

		private async Task<Metadata> CreateAuthenticationHeaders(GrpcChannel channel, InfrastructureHostTestContext context)
		{
			var authenticationClient = new AdminAuthenticationClient(channel);
			var authenticationRequest = new AdminAuthenticationRequest { AccountName = context.TestAccountName, Password = context.TestAccountPassword, HostIdentifier = context.HostIdentifier };

			var call = authenticationClient.AuthenticateAsync(authenticationRequest);
			await call.ResponseAsync.ConfigureAwait(false);
			var authenticationToken = call
				.GetTrailers()
				.SingleOrDefault(trailer => trailer.Key == GrpcHeader.AuthenticationTokenHeaderKey)?.Value;
			var headers = new Metadata {{GrpcHeader.AuthenticationTokenHeaderKey, authenticationToken!}};
			return headers;
		}

		[Fact]
		public async Task Infrastructure_Get_Storage_Local_Admin_TestUser_With_Authentication()
		{
			// Arrange.
			var context = _testContext.Host;
			var channel = context.CreateAdminGrpcInfrastructureChannel();
			var headers = await CreateAuthenticationHeaders(channel, context).ConfigureAwait(false);
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

		[Fact]
		public async Task Infrastructure_Get_Storage_Local_Admin_Admin_With_Authentication()
		{
			// Arrange.
			var context = _testContext.Host;
			var channel = context.CreateAdminGrpcInfrastructureChannel();
			var headers = await CreateAuthenticationHeaders(channel, context).ConfigureAwait(false);
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

		[Fact]
		public async Task Infrastructure_Get_Storage_Local_Admin_System_With_Authentication()
		{
			// Arrange.
			var context = _testContext.Host;
			var channel = context.CreateAdminGrpcInfrastructureChannel();
			var headers = await CreateAuthenticationHeaders(channel, context).ConfigureAwait(false);
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

		[Fact]
        public async Task Infrastructure_Get_Storage_Local_Without_Authentication()
        {
			// Arrange.
	        var channel = _testContext.Host.CreateAdminGrpcInfrastructureChannel();
	        var client = new AdminStorageClient(channel);
	        var request = new AdminStorageRequest();

			// Act.
			var act = new Func<Task>(async () => await client.GetLocalAsync(request));

            // Assert.
            await Assert.ThrowsAsync<RpcException>(act).ConfigureAwait(false); // InvalidInfrastructureOperationException
        }

		[Fact]
		public async Task Infrastructure_Get_Storage_Delayed_Admin_TestUser()
		{
			// Arrange.
			var context = _testContext.Host;
			var channel = context.CreateAdminGrpcInfrastructureChannel();
			var headers = await CreateAuthenticationHeaders(channel, context).ConfigureAwait(false);
			Thread.Sleep(_delay);
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

		[Fact]
		public async Task Infrastructure_Get_Storage_Delayed_Admin_Admin()
		{
			// Arrange.
			var context = _testContext.Host;
			var channel = context.CreateAdminGrpcInfrastructureChannel();
			var headers = await CreateAuthenticationHeaders(channel, context).ConfigureAwait(false);
			Thread.Sleep(_delay);
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

		[Fact]
		public async Task Infrastructure_Get_Storage_Delayed_Admin_System()
		{
			// Arrange.
			var context = _testContext.Host;
			var channel = context.CreateAdminGrpcInfrastructureChannel();
			var headers = await CreateAuthenticationHeaders(channel, context).ConfigureAwait(false);
			Thread.Sleep(_delay);
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

		[Fact]
		public async Task Infrastructure_Get_Storage_Delayed_Without_Authentication_01()
		{
			// Arrange.
			var channel = _testContext.Host.CreateAdminGrpcInfrastructureChannel();
			var client = new AdminStorageClient(channel);
			var request = new AdminStorageRequest();
			Thread.Sleep(_delay);

			// Act.
			var act = new Func<Task>(async () => await client.GetLocalAsync(request));

			// Assert.
			await Assert.ThrowsAsync<RpcException>(act).ConfigureAwait(false); // InvalidInfrastructureOperationException
		}

		[Fact]
		public async Task Infrastructure_Get_Storage_Delayed_Without_Authentication_02()
		{
			// Arrange.
			var channel = _testContext.Host.CreateAdminGrpcInfrastructureChannel();
			Thread.Sleep(_delay);
			var client = new AdminStorageClient(channel);
			var request = new AdminStorageRequest();

			// Act.
			var act = new Func<Task>(async () => await client.GetLocalAsync(request));

			// Assert.
			await Assert.ThrowsAsync<RpcException>(act).ConfigureAwait(false); // InvalidInfrastructureOperationException
		}
    }
}
