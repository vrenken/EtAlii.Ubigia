﻿namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
    using global::Grpc.Core;
    using AdminStorageSingleRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageSingleRequest;
    using AdminStorageMultipleRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageMultipleRequest;
    using Storage = EtAlii.Ubigia.Storage;

    public sealed class GrpcStorageDataClient : IStorageDataClient<IGrpcStorageTransport>
    {
        private StorageGrpcService.StorageGrpcServiceClient _client;
        private IGrpcStorageTransport _transport;

        public async Task<Storage> Add(string storageName, string storageAddress)
        {
            try
            {
                var storage = StorageExtension.ToWire(new Storage
                {
                    Name = storageName,
                    Address = storageAddress,
                });
    
                var request = new AdminStorageSingleRequest
                {
                    Storage = storage,
                };
                var call = _client.PostAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync;
                return response.Storage.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcStorageDataClient)}.Add()", e);
            }
        }

        public async Task Remove(System.Guid storageId)
        {
            try
            {
                var request = new AdminStorageSingleRequest
                {
                    Id = GuidExtension.ToWire(storageId),
                };
                var call = _client.DeleteAsync(request, _transport.AuthenticationHeaders);
                await call.ResponseAsync;
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcStorageDataClient)}.Remove()", e);
            }
        }

        public async Task<Storage> Change(System.Guid storageId, string storageName, string storageAddress)
        {
            try
            {
                var storage = StorageExtension.ToWire(new Storage
                {
                    Id = storageId,
                    Name = storageName,
                    Address = storageAddress,
                });
                
                var request = new AdminStorageSingleRequest
                {
                    Storage = storage,
                };
                var call = _client.PutAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync;
                return response.Storage.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcStorageDataClient)}.Change()", e);
            }
        }

        public async Task<Storage> Get(string storageName)
        {
            try
            {
                var request = new AdminStorageSingleRequest
                {
                    Name = storageName,
                };
                var call = _client.GetSingleAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync;
                return response.Storage?.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcStorageDataClient)}.Get()", e);
            }
        }

        public async Task<Storage> Get(System.Guid storageId)
        {
            try
            {
                var request = new AdminStorageSingleRequest
                {
                    Id = GuidExtension.ToWire(storageId),
                };
                var call = _client.GetSingleAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync;
                return response.Storage?.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcStorageDataClient)}.Get()", e);
            }
        }

        public async IAsyncEnumerable<Storage> GetAll()
        {
            var result = new List<Storage>(); // TODO: AsyncEnumerable
            try
            {
                var request = new AdminStorageMultipleRequest();
                var call = _client.GetMultipleAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync;
                result.AddRange(response.Storages.ToLocal());
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcStorageDataClient)}.GetAll()", e);
            }

            foreach (var item in result)
            {
                yield return item;
            }
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
            _transport = storageConnection.Transport;
            _client = new StorageGrpcService.StorageGrpcServiceClient(_transport.Channel);
            return Task.CompletedTask;
        }

        public Task Disconnect(IStorageConnection<IGrpcStorageTransport> storageConnection)
        {
            _transport = null;
            _client = null;
            return Task.CompletedTask;
        }
    }
}
