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

    public sealed class GrpcSpaceDataClient : ISpaceDataClient<IGrpcStorageTransport>
    {
        private SpaceGrpcService.SpaceGrpcServiceClient _client;

        public async Task<Api.Space> Add(System.Guid accountId, string spaceName, SpaceTemplate template)
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
            var call = _client.PostAsync(request);
            var response = await call.ResponseAsync
                .ConfigureAwait(false);

            return response.Space.ToLocal();
            //return await _invoker.Invoke<Api.Space>(_connection, GrpcHub.Space, "Post", space, template.Name);
        }

        public async Task Remove(System.Guid spaceId)
        {
            var request = new AdminSpaceSingleRequest
            {
                Id = spaceId.ToWire(),
            };
            var call = _client.DeleteAsync(request);
            var response = await call.ResponseAsync
                .ConfigureAwait(false);
            //await _invoker.Invoke(_connection, GrpcHub.Space, "Delete", spaceId);
        }

        public async Task<Api.Space> Change(System.Guid spaceId, string spaceName)
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
            var call = _client.PutAsync(request);
            var response = await call.ResponseAsync
                .ConfigureAwait(false);

            return response.Space.ToLocal();
            //return await _invoker.Invoke<Api.Space>(_connection, GrpcHub.Space, "Put", spaceId, space);
        }

        public async Task<Api.Space> Get(System.Guid accountId, string spaceName)
        {
            var request = new AdminSpaceSingleRequest
            {
                Name = spaceName,
            };
            var call = _client.GetSingleAsync(request);
            var response = await call.ResponseAsync
                .ConfigureAwait(false);

            return response.Space.ToLocal();
            //return await _invoker.Invoke<Api.Space>(_connection, GrpcHub.Space, "GetForAccount", accountId, spaceName);
        }

        public async Task<Api.Space> Get(System.Guid spaceId)
        {
            var request = new AdminSpaceSingleRequest
            {
                Id = spaceId.ToWire(),
            };
            var call = _client.GetSingleAsync(request);
            var response = await call.ResponseAsync
                .ConfigureAwait(false);

            return response.Space.ToLocal();
            //return await _invoker.Invoke<Api.Space>(_connection, GrpcHub.Space, "Get", spaceId);
        }

        public async Task<IEnumerable<Api.Space>> GetAll(System.Guid accountId)
        {
            var request = new AdminSpaceMultipleRequest
            {                               
                AccountId = accountId.ToWire(),
            };
            var call = _client.GetMultipleAsync(request);
            var response = await call.ResponseAsync
                .ConfigureAwait(false);

            return response.Spaces.ToLocal();
            //return await _invoker.Invoke<IEnumerable<Api.Space>>(_connection, GrpcHub.Space, "GetAllForAccount", accountId);
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
            var channel = storageConnection.Transport.Channel;
            _client = new SpaceGrpcService.SpaceGrpcServiceClient(channel);
            return Task.CompletedTask;
      }

        public Task Disconnect(IStorageConnection<IGrpcStorageTransport> storageConnection)
        {
            _client = null;
            return Task.CompletedTask;
        }
    }
}
