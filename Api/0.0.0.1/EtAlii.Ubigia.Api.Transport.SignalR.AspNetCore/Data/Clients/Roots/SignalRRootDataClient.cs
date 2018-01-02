namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR.Client;

    internal class SignalRRootDataClient : SignalRClientBase, IRootDataClient<ISignalRSpaceTransport>
    {
        private IHubProxy _proxy;
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
            return await _invoker.Invoke<Root>(_proxy, SignalRHub.Root, "Post", Connection.Space.Id, root);
        }

        public async Task Remove(Guid rootId)
        {
            await _invoker.Invoke(_proxy, SignalRHub.Root, "Delete", Connection.Space.Id, rootId);
        }

        public async Task<Root> Change(Guid rootId, string rootName)
        {
            var root = new Root
            {
                Id = rootId,
                Name = rootName,
            };

            return await _invoker.Invoke<Root>(_proxy, SignalRHub.Root, "Put", Connection.Space.Id, rootId, root);
        }

        public async Task<Root> Get(string rootName)
        {
            return await _invoker.Invoke<Root>(_proxy, SignalRHub.Root, "GetByName", Connection.Space.Id, rootName);
        }

        public async Task<Root> Get(Guid rootId)
        {
            return await _invoker.Invoke<Root>(_proxy, SignalRHub.Root, "GetById", Connection.Space.Id, rootId);
        }

        public async Task<IEnumerable<Root>> GetAll()
        {
            return await _invoker.Invoke<IEnumerable<Root>>(_proxy, SignalRHub.Root, "GetForSpace", Connection.Space.Id);
        }

        public override async Task Connect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection);

            await Task.Run(() =>
            {
                _proxy = spaceConnection.Transport.HubConnection.CreateHubProxy(SignalRHub.Root);
            });
        }

        public override async Task Disconnect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Disconnect(spaceConnection);

            await Task.Run(() =>
            {
                _proxy = null;
            });
        }
    }
}
