namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using Microsoft.AspNetCore.SignalR.Client;

    public sealed partial class SignalRStorageDataClient : IStorageDataClient<ISignalRStorageTransport>
    {
        private HubConnection _connection;
        private readonly IHubProxyMethodInvoker _invoker;

        public SignalRStorageDataClient(IHubProxyMethodInvoker invoker)
        {
            _invoker = invoker;
        }


        public async Task<Api.Storage> Add(string storageName, string storageAddress)
        {
            var storage = new Api.Storage
            {
                Name = storageName,
                Address = storageAddress,
            };
            return await _invoker.Invoke<Api.Storage>(_connection, SignalRHub.Storage, "Post", storage);
        }

        public async Task Remove(System.Guid storageId)
        {
            await _invoker.Invoke(_connection, SignalRHub.Storage, "Delete", storageId);
        }

        public async Task<Api.Storage> Change(System.Guid storageId, string storageName, string storageAddress)
        {
            var storage = new Api.Storage
            {
                Id = storageId,
                Name = storageName,
                Address = storageAddress,
            };
            return await _invoker.Invoke<Api.Storage>(_connection, SignalRHub.Storage, "Put", storageId, storage);
        }

        public async Task<Api.Storage> Get(string storageName)
        {
            return await _invoker.Invoke<Api.Storage>(_connection, SignalRHub.Storage, "GetByName", storageName);
        }

        public async Task<Api.Storage> Get(System.Guid storageId)
        {
            return await _invoker.Invoke<Api.Storage>(_connection, SignalRHub.Storage, "Get", storageId);
        }

        public async Task<IEnumerable<Api.Storage>> GetAll()
        {
            return await _invoker.Invoke<IEnumerable<Api.Storage>>(_connection, SignalRHub.Storage, "GetAll");
        }

        public async Task Connect(IStorageConnection storageConnection)
        {
            await Connect((IStorageConnection<ISignalRStorageTransport>)storageConnection);
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Disconnect((IStorageConnection<ISignalRStorageTransport>)storageConnection);
        }

        public async Task Connect(IStorageConnection<ISignalRStorageTransport> storageConnection)
        {
            _connection = new HubConnectionFactory().Create(storageConnection.Transport, new Uri(storageConnection.Storage.Address + SignalRHub.BasePath + "/" + SignalRHub.Storage, UriKind.Absolute));
	        await _connection.StartAsync();
        }

        public async Task Disconnect(IStorageConnection<ISignalRStorageTransport> storageConnection)
        {
            await _connection.DisposeAsync();
            _connection = null;
        }
    }
}
