namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
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

        public async Task<Api.Space> Add(System.Guid accountId, string spaceName, SpaceTemplate template)
        {
            var space = new Api.Space
            {
                Name = spaceName,
                AccountId = accountId,
            };
            return await _invoker.Invoke<Api.Space>(_connection, SignalRHub.Space, "Post", space, template.Name);
        }

        public async Task Remove(System.Guid spaceId)
        {
            await _invoker.Invoke(_connection, SignalRHub.Space, "Delete", spaceId);
        }

        public async Task<Api.Space> Change(System.Guid spaceId, string spaceName)
        {
            var space = new Api.Space
            {
                Id = spaceId,
                Name = spaceName,
            };
            return await _invoker.Invoke<Api.Space>(_connection, SignalRHub.Space, "Put", spaceId, space);
        }

        public async Task<Api.Space> Get(System.Guid accountId, string spaceName)
        {
            return await _invoker.Invoke<Api.Space>(_connection, SignalRHub.Space, "GetForAccount", accountId, spaceName);
        }

        public async Task<Api.Space> Get(System.Guid spaceId)
        {
            return await _invoker.Invoke<Api.Space>(_connection, SignalRHub.Space, "Get", spaceId);
        }

        public async Task<IEnumerable<Api.Space>> GetAll(System.Guid accountId)
        {
            return await _invoker.Invoke<IEnumerable<Api.Space>>(_connection, SignalRHub.Space, "GetAllForAccount", accountId);
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
