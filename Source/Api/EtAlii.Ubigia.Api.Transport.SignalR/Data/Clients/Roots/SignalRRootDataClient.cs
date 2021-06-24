// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

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
            return await _invoker.Invoke<Root>(_connection, SignalRHub.Root, "Post", Connection.Space.Id, root).ConfigureAwait(false);
        }

        public async Task Remove(Guid id)
        {
            await _invoker.Invoke(_connection, SignalRHub.Root, "Delete", Connection.Space.Id, id).ConfigureAwait(false);
        }

        public async Task<Root> Change(Guid rootId, string rootName)
        {
            var root = new Root
            {
                Id = rootId,
                Name = rootName,
            };

            return await _invoker.Invoke<Root>(_connection, SignalRHub.Root, "Put", Connection.Space.Id, rootId, root).ConfigureAwait(false);
        }

        public async Task<Root> Get(string rootName)
        {
            return await _invoker.Invoke<Root>(_connection, SignalRHub.Root, "GetByName", Connection.Space.Id, rootName).ConfigureAwait(false);
        }

        public async Task<Root> Get(Guid rootId)
        {
            return await _invoker.Invoke<Root>(_connection, SignalRHub.Root, "GetById", Connection.Space.Id, rootId).ConfigureAwait(false);
        }

        public async IAsyncEnumerable<Root> GetAll()
        {
            var items = _invoker.Stream<Root>(_connection, SignalRHub.Root, "GetForSpace", Connection.Space.Id);
            await foreach (var item in items.ConfigureAwait(false))
            {
                yield return item;
            }
        }

        public override async Task Connect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection).ConfigureAwait(false);

            _connection = new HubConnectionFactory().Create(spaceConnection.Transport, new Uri(spaceConnection.Transport.Address + UriHelper.Delimiter + SignalRHub.Root, UriKind.Absolute));
	        await _connection.StartAsync().ConfigureAwait(false);
        }

        public override async Task Disconnect()
        {
            await base.Disconnect().ConfigureAwait(false); 

            await _connection.DisposeAsync().ConfigureAwait(false);
            _connection = null;
        }
    }
}
