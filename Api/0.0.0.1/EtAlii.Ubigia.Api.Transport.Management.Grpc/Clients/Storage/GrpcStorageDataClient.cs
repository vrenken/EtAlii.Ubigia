namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using AdminStorageSingleRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageSingleRequest;
    using AdminStorageMultipleRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageMultipleRequest;
    using IGrpcStorageTransport = EtAlii.Ubigia.Api.Transport.Grpc.IGrpcStorageTransport;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
    using global::Grpc.Core;

    public sealed partial class GrpcStorageDataClient : IStorageDataClient<IGrpcStorageTransport>
    {
        private StorageGrpcService.StorageGrpcServiceClient _client;
        private IGrpcStorageTransport _transport;

        public async Task<Api.Storage> Add(string storageName, string storageAddress)
        {
            try
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
                var call = _client.PostAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync
                    .ConfigureAwait(false);
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
                    Id = storageId.ToWire(),
                };
                var call = _client.DeleteAsync(request, _transport.AuthenticationHeaders);
                await call.ResponseAsync
                    .ConfigureAwait(false);
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcStorageDataClient)}.Remove()", e);
            }
        }

        public async Task<Api.Storage> Change(System.Guid storageId, string storageName, string storageAddress)
        {
            try
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
                var call = _client.PutAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync
                    .ConfigureAwait(false);
                return response.Storage.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcStorageDataClient)}.Change()", e);
            }
        }

        public async Task<Api.Storage> Get(string storageName)
        {
            try
            {
                var request = new AdminStorageSingleRequest
                {
                    Name = storageName,
                };
                var call = _client.GetSingleAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync
                    .ConfigureAwait(false);
                return response.Storage?.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcStorageDataClient)}.Get()", e);
            }
        }

        public async Task<Api.Storage> Get(System.Guid storageId)
        {
            try
            {
                var request = new AdminStorageSingleRequest
                {
                    Id = storageId.ToWire(),
                };
                var call = _client.GetSingleAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync
                    .ConfigureAwait(false);
                return response.Storage?.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcStorageDataClient)}.Get()", e);
            }
        }

        public async Task<IEnumerable<Api.Storage>> GetAll()
        {
            try
            {
                var request = new AdminStorageMultipleRequest
                {
                };
                var call = _client.GetMultipleAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync
                    .ConfigureAwait(false);
                return response.Storages.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcStorageDataClient)}.GetAll()", e);
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
            _transport = null;
            return Task.CompletedTask;
        }
    }
}
