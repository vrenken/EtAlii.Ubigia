// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
    using global::Grpc.Core;
    using AdminSpacePostSingleRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.SpacePostSingleRequest;
    using AdminSpaceSingleRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.SpaceSingleRequest;
    using AdminSpaceMultipleRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.SpaceMultipleRequest;
    using Space = EtAlii.Ubigia.Space;

    public sealed class GrpcSpaceDataClient : ISpaceDataClient<IGrpcStorageTransport>
    {
        private SpaceGrpcService.SpaceGrpcServiceClient _client;
        private IGrpcStorageTransport _transport;

        public async Task<Space> Add(System.Guid accountId, string spaceName, SpaceTemplate template)
        {
            try
            {
                var space = SpaceExtension.ToWire(new Space
                {
                    Name = spaceName,
                    AccountId = accountId,
                });

                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new AdminSpacePostSingleRequest { Space = space, Template = template.Name };
                var call = _client.PostAsync(request, metadata);
                var response = await call.ResponseAsync.ConfigureAwait(false);

                return response.Space.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcSpaceDataClient)}.Add()", e);
            }
        }

        public async Task Remove(System.Guid spaceId)
        {
            try
            {
                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new AdminSpaceSingleRequest { Id = GuidExtension.ToWire(spaceId) };
                var call = _client.DeleteAsync(request, metadata);
                await call.ResponseAsync.ConfigureAwait(false);
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcSpaceDataClient)}.Remove()", e);
            }
        }

        public async Task<Space> Change(System.Guid spaceId, string spaceName)
        {
            try
            {
                var space = SpaceExtension.ToWire(new Space
                {
                    Id = spaceId,
                    Name = spaceName,
                });

                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new AdminSpaceSingleRequest { Space = space };
                var call = _client.PutAsync(request, metadata);
                var response = await call.ResponseAsync.ConfigureAwait(false);

                return response.Space.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcSpaceDataClient)}.Change()", e);
            }
        }

        public async Task<Space> Get(System.Guid accountId, string spaceName)
        {
            try
            {
                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new AdminSpaceSingleRequest { Name = spaceName };
                var call = _client.GetSingleAsync(request, metadata);
                var response = await call.ResponseAsync.ConfigureAwait(false);

                return response.Space.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcSpaceDataClient)}.Get()", e);
            }
        }

        public async Task<Space> Get(System.Guid spaceId)
        {
            try
            {
                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new AdminSpaceSingleRequest { Id = GuidExtension.ToWire(spaceId) };
                var call = _client.GetSingleAsync(request, metadata);
                var response = await call.ResponseAsync.ConfigureAwait(false);

                return response.Space.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcSpaceDataClient)}.Get()", e);
            }
        }

        public async IAsyncEnumerable<Space> GetAll(System.Guid accountId)
        {
            var metadata = new Metadata { _transport.AuthenticationHeader };
            var request = new AdminSpaceMultipleRequest { AccountId = GuidExtension.ToWire(accountId) };
            var call = _client.GetMultiple(request, metadata);

            // The structure below might seem weird.
            // But it is not possible to combine a try-catch with the yield needed
            // enumerating an IAsyncEnumerable.
            // The only way to solve this is using the enumerator.
            var enumerator = call.ResponseStream
                .ReadAllAsync()
                .GetAsyncEnumerator();
            var hasResult = true;
            while (hasResult)
            {
                SpaceMultipleResponse item;
                try
                {
                    hasResult = await enumerator
                        .MoveNextAsync()
                        .ConfigureAwait(false);
                    item = hasResult ? enumerator.Current : null;
                }
                catch (RpcException e)
                {
                    throw new InvalidInfrastructureOperationException($"{nameof(GrpcSpaceDataClient)}.GetAll()", e);
                }

                if (item != null)
                {
                    yield return item.Space.ToLocal();
                }
            }
        }

        public async Task Connect(IStorageConnection storageConnection)
        {
            await Connect((IStorageConnection<IGrpcStorageTransport>) storageConnection).ConfigureAwait(false);
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Disconnect((IStorageConnection<IGrpcStorageTransport>) storageConnection).ConfigureAwait(false);
        }

        public Task Connect(IStorageConnection<IGrpcStorageTransport> storageConnection)
        {
            _transport = storageConnection.Transport;
            _client = new SpaceGrpcService.SpaceGrpcServiceClient(_transport.CallInvoker);
            return Task.CompletedTask;
        }

        public Task Disconnect(IStorageConnection<IGrpcStorageTransport> storageConnection)
        {
            _transport = null;
            _client = null;
            return Task.CompletedTask;
        }
    }
}
