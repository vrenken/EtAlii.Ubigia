// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Rest
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Rest;

    internal sealed partial class RestStorageDataClient : RestClientBase, IStorageDataClient
    {
        public async Task<Storage> Add(string storageName, string storageAddress)
        {
            var addAddress = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Storages);

            var storage = new Storage
            {
                Name = storageName,
                Address = storageAddress,
            };

            storage = await Connection.Client.Post(addAddress, storage).ConfigureAwait(false);
            return storage;
        }

        public async Task Remove(Guid storageId)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Storages, UriParameter.StorageId, storageId.ToString());
            await Connection.Client.Delete(address).ConfigureAwait(false);
        }

        public async Task<Storage> Change(Guid storageId, string storageName, string storageAddress)
        {
            var storage = new Storage
            {
                Id = storageId,
                Name = storageName,
                Address = storageAddress,
            };

            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Storages, UriParameter.StorageId, storageId.ToString());
            storage = await Connection.Client.Put(address, storage).ConfigureAwait(false);
            return storage;
        }

        public async Task<Storage> Get(string storageName)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Storages, UriParameter.StorageName, storageName);
            var storage = await Connection.Client.Get<Storage>(address).ConfigureAwait(false);
            return storage;
        }

        public async Task<Storage> Get(Guid storageId)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Storages, UriParameter.StorageId, storageId.ToString());
            var storage = await Connection.Client.Get<Storage>(address).ConfigureAwait(false);
            return storage;
        }

        public async IAsyncEnumerable<Storage> GetAll()
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Storages);
            var result = await Connection.Client.Get<IEnumerable<Storage>>(address).ConfigureAwait(false);
            foreach (var item in result)
            {
                yield return item;
            }
        }
    }
}
