namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using global::Grpc.Core;
    using Root = EtAlii.Ubigia.Root;

    internal class GrpcRootDataClient : GrpcClientBase, IRootDataClient<IGrpcSpaceTransport>
    {
        private RootGrpcService.RootGrpcServiceClient _client;
        private IGrpcSpaceTransport _transport;

        public async Task<Root> Add(string name)
        {
            try
            {
                var root = new Root { Name = name };
                var request = new RootPostSingleRequest { Root = root.ToWire(), SpaceId = Connection.Space.Id.ToWire() };
                var response = await _client.PostAsync(request, _transport.AuthenticationHeaders);
                return response.Root.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcRootDataClient)}.Add()", e);
            }
        }

        public async Task Remove(System.Guid id)
        {
            try
            {
                var request = new RootSingleRequest { Id = id.ToWire(), SpaceId = Connection.Space.Id.ToWire() }; 
                await _client.DeleteAsync(request, _transport.AuthenticationHeaders);
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcRootDataClient)}.Remove()", e);
            }
        }

        public async Task<Root> Change(System.Guid rootId, string rootName)
        {
            try
            {
                var root = new Root { Id = rootId, Name = rootName };
                var request = new RootSingleRequest { Root = root.ToWire(), SpaceId = Connection.Space.Id.ToWire() };
                var response = await _client.PutAsync(request, _transport.AuthenticationHeaders);
                return response.Root.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcRootDataClient)}.Change()", e);
            }
        }

        public async Task<Root> Get(string rootName)
        {
            try
            {
                var request = new RootSingleRequest { Name = rootName, SpaceId = Connection.Space.Id.ToWire() }; 
                var response = await _client.GetSingleAsync(request, _transport.AuthenticationHeaders);
                return response.Root?.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcRootDataClient)}.Get()", e);
            }
        }

        public async Task<Root> Get(System.Guid rootId)
        {
            try
            {
                var request = new RootSingleRequest { Id = rootId.ToWire(), SpaceId = Connection.Space.Id.ToWire() }; 
                var response = await _client.GetSingleAsync(request, _transport.AuthenticationHeaders);
                return response.Root?.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcRootDataClient)}.Get()", e);
            }
        }

        public async IAsyncEnumerable<Root> GetAll()
        {
            var result = new List<Root>(); 
            try
            {
                var request = new RootMultipleRequest { SpaceId = Connection.Space.Id.ToWire() }; 
                var call = _client.GetMultiple(request, _transport.AuthenticationHeaders);
                await foreach (var response in call.ResponseStream.ReadAllAsync())
                {
                    result.Add(response.Root.ToLocal()); // TODO: AsyncEnumerable 
                }
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcRootDataClient)}.GetAll()", e);
            }

            foreach (var item in result)
            {
                yield return item; // TODO: AsyncEnumerable - refactor to grpc stream?
            }
        }

        public override async Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection);
            
            _transport = ((IGrpcSpaceConnection)spaceConnection).Transport;
            _client = new RootGrpcService.RootGrpcServiceClient(_transport.Channel);
        }

        public override async Task Disconnect()
        {
            await base.Disconnect();
            _transport = null;
            _client = null;
        }
    }
}
