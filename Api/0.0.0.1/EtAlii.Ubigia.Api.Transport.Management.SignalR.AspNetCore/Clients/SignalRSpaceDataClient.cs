namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
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
            return await _invoker.Invoke<Space>(_connection, SignalRHub.Space, "Post", space, template.Name);
        }

        public async Task Remove(Guid spaceId)
        {
            await _invoker.Invoke(_connection, SignalRHub.Space, "Delete", spaceId);
        }

        public async Task<Space> Change(Guid spaceId, string spaceName)
        {
            var space = new Space
            {
                Id = spaceId,
                Name = spaceName,
            };
            return await _invoker.Invoke<Space>(_connection, SignalRHub.Space, "Put", spaceId, space);
        }

        public async Task<Space> Get(Guid accountId, string spaceName)
        {
            return await _invoker.Invoke<Space>(_connection, SignalRHub.Space, "GetForAccount", accountId, spaceName);
        }

        public async Task<Space> Get(Guid spaceId)
        {
            return await _invoker.Invoke<Space>(_connection, SignalRHub.Space, "Get", spaceId);
        }

        public async Task<IEnumerable<Space>> GetAll(Guid accountId)
        {
            return await _invoker.Invoke<IEnumerable<Space>>(_connection, SignalRHub.Space, "GetAllForAccount", accountId);
        }

        public async Task Connect(IStorageConnection storageConnection)
        {
            await Connect((IStorageConnection<ISignalRStorageTransport>) storageConnection);
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Disconnect((IStorageConnection<ISignalRStorageTransport>) storageConnection);
        }

        public async Task Connect(IStorageConnection<ISignalRStorageTransport> storageConnection)
        {
            _connection = new HubConnectionFactory().Create(storageConnection.Transport, new Uri(storageConnection.Storage.Address + SignalRHub.BasePath + "/" + SignalRHub.Space, UriKind.Absolute));
			await _connection.StartAsync();
        }

        public async Task Disconnect(IStorageConnection<ISignalRStorageTransport> storageConnection)
        {
            await _connection.DisposeAsync();
            _connection = null;
        }
    }
}
