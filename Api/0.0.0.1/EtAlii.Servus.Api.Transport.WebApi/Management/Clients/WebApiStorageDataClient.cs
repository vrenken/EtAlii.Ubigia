namespace EtAlii.Servus.Api.Management.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.WebApi;

    internal sealed partial class WebApiStorageDataClient : WebApiClientBase, IStorageDataClient
    {
        public WebApiStorageDataClient()
        {
        }

        public async Task<Storage> Add(string storageName, string storageAddress)
        {
            var addAddress = Connection.AddressFactory.Create(Connection.Storage, RelativeUri.Storages);

            var storage = new Storage
            {
                Name = storageName,
                Address = storageAddress,
            };

            storage = await Connection.Client.Post<Storage>(addAddress, storage);
            return storage;
        }

        public async Task Remove(Guid storageId)
        {
            var address = Connection.AddressFactory.Create(Connection.Storage, RelativeUri.Storages, UriParameter.StorageId, storageId.ToString());
            await Connection.Client.Delete(address);
        }

        public async Task<Storage> Change(Guid storageId, string storageName, string storageAddress)
        {
            var storage = new Storage
            {
                Id = storageId,
                Name = storageName,
                Address = storageAddress,
            };

            var address = Connection.AddressFactory.Create(Connection.Storage, RelativeUri.Storages, UriParameter.StorageId, storageId.ToString());
            storage = await Connection.Client.Put(address, storage);
            return storage;
        }

        public async Task<Storage> Get(string storageName)
        {
            var address = Connection.AddressFactory.Create(Connection.Storage, RelativeUri.Storages, UriParameter.StorageName, storageName);
            var storage = await Connection.Client.Get<Storage>(address);
            return storage;
        }

        public async Task<Storage> Get(Guid storageId)
        {
            var address = Connection.AddressFactory.Create(Connection.Storage, RelativeUri.Storages, UriParameter.StorageId, storageId.ToString());
            var storage = await Connection.Client.Get<Storage>(address);
            return storage;
        }

        public async Task<IEnumerable<Storage>> GetAll()
        {
            var address = Connection.AddressFactory.Create(Connection.Storage, RelativeUri.Storages);
            var storages = await Connection.Client.Get<IEnumerable<Storage>>(address);
            return storages;
        }
    }
}
