﻿namespace EtAlii.Ubigia.Api.Management.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using Microsoft.AspNet.SignalR.Client;

    public sealed partial class SignalRStorageDataClient : IStorageDataClient<ISignalRStorageTransport>
    {
        private IHubProxy _proxy;
        private readonly IHubProxyMethodInvoker _invoker;

        public SignalRStorageDataClient(IHubProxyMethodInvoker invoker)
        {
            _invoker = invoker;
        }


        public async Task<Storage> Add(string storageName, string storageAddress)
        {
            var storage = new Storage
            {
                Name = storageName,
                Address = storageAddress,
            };
            return await _invoker.Invoke<Storage>(_proxy, SignalRHub.Storage, "Post", storage);
        }

        public async Task Remove(Guid storageId)
        {
            await _invoker.Invoke(_proxy, SignalRHub.Storage, "Delete", storageId);
        }

        public async Task<Storage> Change(Guid storageId, string storageName, string storageAddress)
        {
            var storage = new Storage
            {
                Id = storageId,
                Name = storageName,
                Address = storageAddress,
            };
            return await _invoker.Invoke<Storage>(_proxy, SignalRHub.Storage, "Put", storageId, storage);
        }

        public async Task<Storage> Get(string storageName)
        {
            return await _invoker.Invoke<Storage>(_proxy, SignalRHub.Storage, "GetByName", storageName);
        }

        public async Task<Storage> Get(Guid storageId)
        {
            return await _invoker.Invoke<Storage>(_proxy, SignalRHub.Storage, "Get", storageId);
        }

        public async Task<IEnumerable<Storage>> GetAll()
        {
            return await _invoker.Invoke<IEnumerable<Storage>>(_proxy, SignalRHub.Storage, "GetAll");
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
            await Task.Run(() =>
            {
                _proxy = storageConnection.Transport.HubConnection.CreateHubProxy(SignalRHub.Storage);
            });
        }

        public async Task Disconnect(IStorageConnection<ISignalRStorageTransport> storageConnection)
        {
            await Task.Run(() =>
            {
                _proxy = null;
            });
        }
    }
}
