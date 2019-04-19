namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;

    internal class SignalRRootDataClient : SignalRClientBase, IRootDataClient<ISignalRSpaceTransport>
    {
        private HubConnection _connection;
        private readonly IHubProxyMethodInvoker _invoker;

        public SignalRRootDataClient(IHubProxyMethodInvoker invoker)
        {
            _invoker = invoker;
        }

        public async Task<Root> Add(string name)
        {
            var root = new Root
            {
                Name = name,
            };
            return await _invoker.Invoke<Root>(_connection, SignalRHub.Root, "Post", Connection.Space.Id, root);
        }

        public async Task Remove(Guid id)
        {
            await _invoker.Invoke(_connection, SignalRHub.Root, "Delete", Connection.Space.Id, id);
        }

        public async Task<Root> Change(Guid rootId, string rootName)
        {
            var root = new Root
            {
                Id = rootId,
                Name = rootName,
            };

            return await _invoker.Invoke<Root>(_connection, SignalRHub.Root, "Put", Connection.Space.Id, rootId, root);
        }

        public async Task<Root> Get(string rootName)
        {
            return await _invoker.Invoke<Root>(_connection, SignalRHub.Root, "GetByName", Connection.Space.Id, rootName);
        }

        public async Task<Root> Get(Guid rootId)
        {
            return await _invoker.Invoke<Root>(_connection, SignalRHub.Root, "GetById", Connection.Space.Id, rootId);
        }

        public async Task<IEnumerable<Root>> GetAll()
        {
            return await _invoker.Invoke<IEnumerable<Root>>(_connection, SignalRHub.Root, "GetForSpace", Connection.Space.Id);
        }

        public override async Task Connect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection);

            _connection = new HubConnectionFactory().Create(spaceConnection.Transport, new Uri(spaceConnection.Storage.Address + SignalRHub.BasePath + SignalRHub.Root, UriKind.Absolute));
	        await _connection.StartAsync();
        }

        public override async Task Disconnect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Disconnect(spaceConnection);

            await _connection.DisposeAsync();
            _connection = null;
        }
    }
}
