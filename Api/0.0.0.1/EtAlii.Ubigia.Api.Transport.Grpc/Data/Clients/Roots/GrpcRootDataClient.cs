namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class GrpcRootDataClient : GrpcClientBase, IRootDataClient<IGrpcSpaceTransport>
    {
        //private HubConnection _connection;
        //private readonly IHubProxyMethodInvoker _invoker;

        //public GrpcRootDataClient(IHubProxyMethodInvoker invoker)
        //{
        //    _invoker = invoker;
        //}

        public async Task<Root> Add(string name)
        {
            var root = new Root
            {
                Name = name,
            };
            // TODO: GRPC
            return await Task.FromResult<Root>(null);
            //return await _invoker.Invoke<Root>(_connection, GrpcHub.Root, "Post", Connection.Space.Id, root);
        }

        public async Task Remove(Guid rootId)
        {
            // TODO: GRPC
            //await _invoker.Invoke(_connection, GrpcHub.Root, "Delete", Connection.Space.Id, rootId);
        }

        public async Task<Root> Change(Guid rootId, string rootName)
        {
            var root = new Root
            {
                Id = rootId,
                Name = rootName,
            };

            // TODO: GRPC
            return await Task.FromResult<Root>(null);
            //return await _invoker.Invoke<Root>(_connection, GrpcHub.Root, "Put", Connection.Space.Id, rootId, root);
        }

        public async Task<Root> Get(string rootName)
        {
            // TODO: GRPC
            return await Task.FromResult<Root>(null);
            //return await _invoker.Invoke<Root>(_connection, GrpcHub.Root, "GetByName", Connection.Space.Id, rootName);
        }

        public async Task<Root> Get(Guid rootId)
        {
            // TODO: GRPC
            return await Task.FromResult<Root>(null);
            //return await _invoker.Invoke<Root>(_connection, GrpcHub.Root, "GetById", Connection.Space.Id, rootId);
        }

        public async Task<IEnumerable<Root>> GetAll()
        {
            // TODO: GRPC
            return await Task.FromResult<IEnumerable<Root>>(null);
            //return await _invoker.Invoke<IEnumerable<Root>>(_connection, GrpcHub.Root, "GetForSpace", Connection.Space.Id);
        }

        public override async Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection);

            // TODO: GRPC
            //_connection = new HubConnectionFactory().Create(spaceConnection.Transport, new Uri(spaceConnection.Storage.Address + GrpcHub.BasePath + "/" + GrpcHub.Root, UriKind.Absolute));
	        //await _connection.StartAsync();
        }

        public override async Task Disconnect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            await base.Disconnect(spaceConnection);

            // TODO: GRPC
            //await _connection.DisposeAsync();
            //_connection = null;
        }
    }
}
