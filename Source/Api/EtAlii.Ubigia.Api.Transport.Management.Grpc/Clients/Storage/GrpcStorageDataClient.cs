// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
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

                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new AdminStorageSingleRequest { Storage = storage };
                var call = _client.PostAsync(request, metadata);
                var response = await call.ResponseAsync.ConfigureAwait(false);
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
                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new AdminStorageSingleRequest { Id = GuidExtension.ToWire(storageId) };
                var call = _client.DeleteAsync(request, metadata);
                await call.ResponseAsync.ConfigureAwait(false);
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

                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new AdminStorageSingleRequest { Storage = storage };
                var call = _client.PutAsync(request, metadata);
                var response = await call.ResponseAsync.ConfigureAwait(false);
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
                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new AdminStorageSingleRequest { Name = storageName };
                var call = _client.GetSingleAsync(request, metadata);
                var response = await call.ResponseAsync.ConfigureAwait(false);
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
                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new AdminStorageSingleRequest { Id = GuidExtension.ToWire(storageId) };
                var call = _client.GetSingleAsync(request, metadata);
                var response = await call.ResponseAsync.ConfigureAwait(false);
                return response.Storage?.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcStorageDataClient)}.Get()", e);
            }
        }

        public async IAsyncEnumerable<Storage> GetAll()
        {
            var metadata = new Metadata { _transport.AuthenticationHeader };
            var request = new AdminStorageMultipleRequest();
            var call = _client.GetMultiple(request, metadata);

            // The structure below might seem weird.
            // But it is not possible to combine a try-catch with the yield needed
            // enumerating an IAsyncEnumerable.
            // The only way to solve this is using the enumerator.
            var enumerator = call.ResponseStream
                .ReadAllAsync()
                .GetAsyncEnumerator();
            var hasResult = true;
            while (hasResult)
            {
                StorageMultipleResponse item;
                try
                {
                    hasResult = await enumerator
                        .MoveNextAsync()
                        .ConfigureAwait(false);
                    item = hasResult ? enumerator.Current : null;
                }
                catch (RpcException e)
                {
                    throw new InvalidInfrastructureOperationException($"{nameof(GrpcSpaceDataClient)}.GetAll()", e);
                }

                if (item != null)
                {
                    yield return item.Storage.ToLocal();
                }
            }
        }

        public async Task Connect(IStorageConnection storageConnection)
        {
            await Connect((IStorageConnection<IGrpcStorageTransport>)storageConnection).ConfigureAwait(false);
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Disconnect((IStorageConnection<IGrpcStorageTransport>)storageConnection).ConfigureAwait(false);
        }

        public Task Connect(IStorageConnection<IGrpcStorageTransport> storageConnection)
        {
            _transport = storageConnection.Transport;
            _client = new StorageGrpcService.StorageGrpcServiceClient(_transport.CallInvoker);
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
