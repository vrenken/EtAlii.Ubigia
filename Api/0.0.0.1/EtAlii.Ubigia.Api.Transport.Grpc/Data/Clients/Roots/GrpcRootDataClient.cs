namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;

    internal class GrpcRootDataClient : GrpcClientBase, IRootDataClient<IGrpcSpaceTransport>
    {
        private RootGrpcService.RootGrpcServiceClient _client;
        private IGrpcSpaceConnection _connection;

        public async Task<Api.Root> Add(string name)
        {
            var root = new Api.Root
            {
                Name = name,
            };
            var request = new RootSingleRequest { Root = root.ToWire(), SpaceId = Connection.Space.Id.ToWire() };
            var response = await _client.PutAsync(request,_connection.Transport.AuthenticationHeaders);
            return response.Root.ToLocal();
        }

        public async Task Remove(System.Guid rootId)
        {
            var request = new RootSingleRequest { Id = rootId.ToWire(), SpaceId = Connection.Space.Id.ToWire()}; 
            await _client.DeleteAsync(request, _connection.Transport.AuthenticationHeaders);
        }

        public async Task<Api.Root> Change(System.Guid rootId, string rootName)
        {
            var root = new Api.Root
            {
                Id = rootId,
                Name = rootName,
            };
            var request = new RootPostSingleRequest { Root = root.ToWire(), SpaceId = Connection.Space.Id.ToWire() };
            var response = await _client.PostAsync(request, _connection.Transport.AuthenticationHeaders);
            return response.Root.ToLocal();
        }

        public async Task<Api.Root> Get(string rootName)
        {
            var request = new RootSingleRequest { Name = rootName, SpaceId = Connection.Space.Id.ToWire() }; 
            var response = await _client.GetSingleAsync(request, _connection.Transport.AuthenticationHeaders);
            return response.Root.ToLocal();
        }

        public async Task<Api.Root> Get(System.Guid rootId)
        {
            var request = new RootSingleRequest { Id = rootId.ToWire(), SpaceId = Connection.Space.Id.ToWire() }; 
            var response = await _client.GetSingleAsync(request, _connection.Transport.AuthenticationHeaders);
            return response.Root.ToLocal();
        }

        public async Task<IEnumerable<Api.Root>> GetAll()
        {
            var request = new RootMultipleRequest { SpaceId = Connection.Space.Id.ToWire()}; 
            var response = await _client.GetMultipleAsync(request, _connection.Transport.AuthenticationHeaders);
            return response.Roots.ToLocal();
        }

        public override Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            var channel = spaceConnection.Transport.Channel;
            _connection = (IGrpcSpaceConnection)spaceConnection;
            _client = new RootGrpcService.RootGrpcServiceClient(channel);
            return Task.CompletedTask;
        }

        public override Task Disconnect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            _connection = null;
            _client = null;
            return Task.CompletedTask;
        }
    }
}
