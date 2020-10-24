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
                
                var request = new AdminSpacePostSingleRequest
                {
                    Space = space,
                    Template = template.Name
                };
                var call = _client.PostAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync;
    
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
                    Id = GuidExtension.ToWire(spaceId),
                };
                var call = _client.DeleteAsync(request, _transport.AuthenticationHeaders);
                await call.ResponseAsync;
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
                
                var request = new AdminSpaceSingleRequest
                {
                    Space = space,
                };
                var call = _client.PutAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync;
    
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
                var request = new AdminSpaceSingleRequest
                {
                    Name = spaceName,
                };
                var call = _client.GetSingleAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync;
    
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
                var request = new AdminSpaceSingleRequest
                {
                    Id = GuidExtension.ToWire(spaceId),
                };
                var call = _client.GetSingleAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync;
    
                return response.Space.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcSpaceDataClient)}.Get()", e);
            }
        }

        public async IAsyncEnumerable<Space> GetAll(System.Guid accountId)
        {
            var result = new List<Space>();
            try
            {
                var request = new AdminSpaceMultipleRequest
                {                               
                    AccountId = GuidExtension.ToWire(accountId),
                };
                var call = _client.GetMultipleAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync;
    
                result.AddRange(response.Spaces.ToLocal());
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcSpaceDataClient)}.GetAll()", e);
            }

            foreach (var item in result)
            {
                yield return item;
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
