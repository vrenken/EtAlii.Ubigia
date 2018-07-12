namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using AdminSpacePostSingleRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.SpacePostSingleRequest;
    using AdminSpaceSingleRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.SpaceSingleRequest;
    using AdminSpaceMultipleRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.SpaceMultipleRequest;
    using IGrpcStorageTransport = EtAlii.Ubigia.Api.Transport.Grpc.IGrpcStorageTransport;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
    using global::Grpc.Core;

    public sealed class GrpcSpaceDataClient : ISpaceDataClient<IGrpcStorageTransport>
    {
        private SpaceGrpcService.SpaceGrpcServiceClient _client;
        private IGrpcStorageTransport _transport;

        public async Task<Api.Space> Add(System.Guid accountId, string spaceName, SpaceTemplate template)
        {
            try
            {
                var space = new Api.Space 
                {
                    Name = spaceName,
                    AccountId = accountId,
                }.ToWire();
                
                var request = new AdminSpacePostSingleRequest
                {
                    Space = space,
                    Template = template.Name
                };
                var call = _client.PostAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync
                    .ConfigureAwait(false);
    
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
                var request = new AdminSpaceSingleRequest
                {
                    Id = spaceId.ToWire(),
                };
                var call = _client.DeleteAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync
                    .ConfigureAwait(false);
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcSpaceDataClient)}.Remove()", e);
            }
        }

        public async Task<Api.Space> Change(System.Guid spaceId, string spaceName)
        {
            try
            {
                var space = new Api.Space
                {
                    Id = spaceId,
                    Name = spaceName,
                }.ToWire();
                
                var request = new AdminSpaceSingleRequest
                {
                    Space = space,
                };
                var call = _client.PutAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync
                    .ConfigureAwait(false);
    
                return response.Space.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcSpaceDataClient)}.Change()", e);
            }
        }

        public async Task<Api.Space> Get(System.Guid accountId, string spaceName)
        {
            try
            {
                var request = new AdminSpaceSingleRequest
                {
                    Name = spaceName,
                };
                var call = _client.GetSingleAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync
                    .ConfigureAwait(false);
    
                return response.Space.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcSpaceDataClient)}.Get()", e);
            }
        }

        public async Task<Api.Space> Get(System.Guid spaceId)
        {
            try
            {
                var request = new AdminSpaceSingleRequest
                {
                    Id = spaceId.ToWire(),
                };
                var call = _client.GetSingleAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync
                    .ConfigureAwait(false);
    
                return response.Space.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcSpaceDataClient)}.Get()", e);
            }
        }

        public async Task<IEnumerable<Api.Space>> GetAll(System.Guid accountId)
        {
            try
            {
                var request = new AdminSpaceMultipleRequest
                {                               
                    AccountId = accountId.ToWire(),
                };
                var call = _client.GetMultipleAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync
                    .ConfigureAwait(false);
    
                return response.Spaces.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcSpaceDataClient)}.GetAll()", e);
            }
        }

        public async Task Connect(IStorageConnection storageConnection)
        {
            await Connect((IStorageConnection<IGrpcStorageTransport>) storageConnection);
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Disconnect((IStorageConnection<IGrpcStorageTransport>) storageConnection);
        }

        public Task Connect(IStorageConnection<IGrpcStorageTransport> storageConnection)
        {
            _transport = storageConnection.Transport;
            _client = new SpaceGrpcService.SpaceGrpcServiceClient(_transport.Channel);
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
