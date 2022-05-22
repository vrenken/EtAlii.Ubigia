// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;

    public class AdminStorageService : StorageGrpcService.StorageGrpcServiceBase, IAdminStorageService
    {
        private readonly IStorageRepository _items;

        public AdminStorageService(IStorageRepository items)
        {
            _items = items;
        }

        public override async Task<StorageSingleResponse> GetLocal(StorageSingleRequest request, ServerCallContext context)
        {
            var storage = await _items.GetLocal().ConfigureAwait(false);

            var response = new StorageSingleResponse
            {
                Storage = storage.ToWire()
            };
            return response;
        }

        //public Storage GetByName(string storageName)
        public override async Task<StorageSingleResponse> GetSingle(StorageSingleRequest request, ServerCallContext context)
        {
            EtAlii.Ubigia.Storage storage;

            switch (request)
            {
                case var _ when request.Id != null: // Get Item by id
                    storage = await _items.Get(request.Id.ToLocal()).ConfigureAwait(false);
                    break;
                case var _ when request.Storage != null: // Get Item by id
                    storage = await _items.Get(request.Storage.Id.ToLocal()).ConfigureAwait(false);
                    break;
                case var _ when !string.IsNullOrWhiteSpace(request.Name): // Get Item by name
                    storage = await _items.Get(request.Name).ConfigureAwait(false);
                    break;
                default:
                    throw new InvalidOperationException("Unable to serve a Storage GET client request");
            }

            var response = new StorageSingleResponse
            {
                Storage = storage?.ToWire()
            };
            return response;
        }

        // Get all Items
        public override async Task GetMultiple(StorageMultipleRequest request, IServerStreamWriter<StorageMultipleResponse> responseStream, ServerCallContext context)
        {
            var items = _items
                .GetAll()
                .ConfigureAwait(false);
            await foreach (var item in items)
            {
                var response = new StorageMultipleResponse
                {
                    Storage = item.ToWire()
                };
                await responseStream.WriteAsync(response).ConfigureAwait(false);
            }
        }

        public override async Task<StorageSingleResponse> Post(StorageSingleRequest request, ServerCallContext context)
        {
            var storage = request.Storage.ToLocal();
            storage = await _items.Add(storage).ConfigureAwait(false);
            var response = new StorageSingleResponse
            {
                Storage = storage.ToWire()
            };
            return response;
        }

        // Add item
        public override async Task<StorageSingleResponse> Put(StorageSingleRequest request, ServerCallContext context)
        {
            var storage = request.Storage.ToLocal();
            storage = await _items.Update(storage.Id, storage).ConfigureAwait(false);

            var response = new StorageSingleResponse
            {
                Storage = storage.ToWire()
            };
            return response;
        }

        // Update Item by id
        public override async Task<StorageSingleResponse> Delete(StorageSingleRequest request, ServerCallContext context)
        {
            switch (request)
            {
                case var _ when request.Id != null: // Get Item by id
                    await _items.Remove(request.Id.ToLocal()).ConfigureAwait(false);
                    break;
                case var _ when request.Storage != null: // Get Item by id
                    await _items.Remove(request.Storage.Id.ToLocal()).ConfigureAwait(false);
                    break;
                default:
                    throw new InvalidOperationException("Unable to serve a Storage DELETE client request");
            }

            var response = new StorageSingleResponse
            {
                Storage = request.Storage
            };
            return response;
        }
    }
}
