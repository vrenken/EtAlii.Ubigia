﻿namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using AdminStorageSingleRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageSingleRequest;
    using AdminStorageMultipleRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageMultipleRequest;
    using IGrpcStorageTransport = EtAlii.Ubigia.Api.Transport.Grpc.IGrpcStorageTransport;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;

    public sealed partial class GrpcStorageDataClient : IStorageDataClient<IGrpcStorageTransport>
    {
        private StorageGrpcService.StorageGrpcServiceClient _client;

        public async Task<Api.Storage> Add(string storageName, string storageAddress)
        {
            var storage = new Api.Storage
            {
                Name = storageName,
                Address = storageAddress,
            }.ToWire();

            var request = new AdminStorageSingleRequest
            {
                Storage = storage,
            };
            var call = _client.PostAsync(request);
            var response = await call.ResponseAsync;
            return response.Storage.ToLocal();
            //return await _invoker.Invoke<Api.Storage>(_connection, GrpcHub.Storage, "Post", storage);
        }

        public async Task Remove(System.Guid storageId)
        {
            var request = new AdminStorageSingleRequest
            {
                Id = storageId.ToWire(),
            };
            var call = _client.DeleteAsync(request);
            await call.ResponseAsync;
            //return response.Storage.ToLocal();
            //await _invoker.Invoke(_connection, GrpcHub.Storage, "Delete", storageId);
        }

        public async Task<Api.Storage> Change(System.Guid storageId, string storageName, string storageAddress)
        {
            var storage = new Api.Storage
            {
                Id = storageId,
                Name = storageName,
                Address = storageAddress,
            }.ToWire();
            
            var request = new AdminStorageSingleRequest
            {
                Storage = storage,
            };
            var call = _client.PutAsync(request);
            var response = await call.ResponseAsync;
            return response.Storage.ToLocal();
            //return await _invoker.Invoke<Api.Storage>(_connection, GrpcHub.Storage, "Put", storageId, storage);
        }

        public async Task<Api.Storage> Get(string storageName)
        {
            var request = new AdminStorageSingleRequest
            {
                Name = storageName,
            };
            var call = _client.GetSingleAsync(request);
            var response = await call.ResponseAsync;
            return response.Storage.ToLocal();
            //return await _invoker.Invoke<Api.Storage>(_connection, GrpcHub.Storage, "GetByName", storageName);
        }

        public async Task<Api.Storage> Get(System.Guid storageId)
        {
            var request = new AdminStorageSingleRequest
            {
                Id = storageId.ToWire(),
            };
            var call = _client.GetSingleAsync(request);
            var response = await call.ResponseAsync;
            return response.Storage.ToLocal();
            //return await _invoker.Invoke<Api.Storage>(_connection, GrpcHub.Storage, "Get", storageId);
        }

        public async Task<IEnumerable<Api.Storage>> GetAll()
        {
            var request = new AdminStorageMultipleRequest
            {
            };
            var call = _client.GetMultipleAsync(request);
            var response = await call.ResponseAsync;
            return response.Storages.ToLocal();
            //return await _invoker.Invoke<IEnumerable<Api.Storage>>(_connection, GrpcHub.Storage, "GetAll");
        }

        public async Task Connect(IStorageConnection storageConnection)
        {
            await Connect((IStorageConnection<IGrpcStorageTransport>)storageConnection);
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Disconnect((IStorageConnection<IGrpcStorageTransport>)storageConnection);
        }

        public Task Connect(IStorageConnection<IGrpcStorageTransport> storageConnection)
        {
            var channel = storageConnection.Transport.Channel;
            _client = new StorageGrpcService.StorageGrpcServiceClient(channel);
            return Task.CompletedTask;
        }

        public Task Disconnect(IStorageConnection<IGrpcStorageTransport> storageConnection)
        {
            _client = null;
            return Task.CompletedTask;
        }
    }
}
