// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using Microsoft.AspNetCore.SignalR.Client;

    public sealed class SignalRSpaceDataClient : ISpaceDataClient<ISignalRStorageTransport>
    {
        private HubConnection _connection;
        private readonly IHubProxyMethodInvoker _invoker;

        public SignalRSpaceDataClient(IHubProxyMethodInvoker invoker)
        {
            _invoker = invoker;
        }

        public async Task<Space> Add(Guid accountId, string spaceName, SpaceTemplate template)
        {
            var space = new Space
            {
                Name = spaceName,
                AccountId = accountId,
            };
            return await _invoker.Invoke<Space>(_connection, SignalRHub.Space, "Post", space, template.Name).ConfigureAwait(false);
        }

        public async Task Remove(Guid spaceId)
        {
            await _invoker.Invoke(_connection, SignalRHub.Space, "Delete", spaceId).ConfigureAwait(false);
        }

        public async Task<Space> Change(Guid spaceId, string spaceName)
        {
            var space = new Space
            {
                Id = spaceId,
                Name = spaceName,
            };
            return await _invoker.Invoke<Space>(_connection, SignalRHub.Space, "Put", spaceId, space).ConfigureAwait(false);
        }

        public async Task<Space> Get(Guid accountId, string spaceName)
        {
            return await _invoker.Invoke<Space>(_connection, SignalRHub.Space, "GetForAccount", accountId, spaceName).ConfigureAwait(false);
        }

        public async Task<Space> Get(Guid spaceId)
        {
            return await _invoker.Invoke<Space>(_connection, SignalRHub.Space, "Get", spaceId).ConfigureAwait(false);
        }

        public async IAsyncEnumerable<Space> GetAll(Guid accountId)
        {
            var items = _invoker.Stream<Space>(_connection, SignalRHub.Space, "GetAllForAccount", accountId);
            await foreach (var item in items.ConfigureAwait(false))
            {
                yield return item;
            }
        }

        public async Task Connect(IStorageConnection storageConnection)
        {
            await Connect((IStorageConnection<ISignalRStorageTransport>) storageConnection).ConfigureAwait(false);
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Disconnect((IStorageConnection<ISignalRStorageTransport>) storageConnection).ConfigureAwait(false);
        }

        public async Task Connect(IStorageConnection<ISignalRStorageTransport> storageConnection)
        {
            _connection = new HubConnectionFactory().Create(storageConnection.Transport, new Uri(storageConnection.Transport.Address + UriHelper.Delimiter + SignalRHub.Space, UriKind.Absolute));
			await _connection.StartAsync().ConfigureAwait(false);
        }

        public async Task Disconnect(IStorageConnection<ISignalRStorageTransport> storageConnection)
        {
            await _connection.DisposeAsync().ConfigureAwait(false);
            _connection = null;
        }
    }
}
