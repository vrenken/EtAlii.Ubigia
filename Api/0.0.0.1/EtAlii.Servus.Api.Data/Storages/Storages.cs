namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;

    public class Storages : CollectionBase<StorageConnection>
    {
        internal Storages(StorageConnection connection)
            : base(connection)
        {
        }

        private const string _relativePath = "management/storage";

        public Storage Add(string storageName, string storageAddress)
        {
            if (Connection.CurrentStorage == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            var addAddress = AddressFactory.Create(Connection.CurrentStorage, _relativePath);

            var storage = new Storage
            {
                Name = storageName,
                Address = storageAddress,
            };

            storage = Infrastructure.Post<Storage>(addAddress, storage);
            return storage;
        }

        public void Remove(Guid storageId)
        {
            if (Connection.CurrentStorage == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            var address = AddressFactory.Create(Connection.CurrentStorage, _relativePath, UriParameter.StorageId, storageId.ToString());
            Infrastructure.Delete(address);
        }

        public Storage Change(Guid storageId, string storageName, string storageAddress)
        {
            if (Connection.CurrentStorage == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            var storage = new Storage
            {
                Id = storageId,
                Name = storageName,
                Address = storageAddress,
            };

            var address = AddressFactory.Create(Connection.CurrentStorage, _relativePath, UriParameter.StorageId, storageId.ToString());
            storage = Infrastructure.Put(address, storage);
            return storage;
        }

        public Storage Get(string storageName)
        {
            if (Connection.CurrentStorage == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            var address = AddressFactory.Create(Connection.CurrentStorage, _relativePath, UriParameter.StorageName, storageName);
            var storage = Infrastructure.Get<Storage>(address);
            return storage;
        }

        public Storage Get(Guid storageId)
        {
            if (Connection.CurrentStorage == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            var address = AddressFactory.Create(Connection.CurrentStorage, _relativePath, UriParameter.StorageId, storageId.ToString());
            var storage = Infrastructure.Get<Storage>(address);
            return storage;
        }

        public IEnumerable<Storage> GetAll()
        {
            if (Connection.CurrentStorage == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            var address = AddressFactory.Create(Connection.CurrentStorage, _relativePath);
            var storages = Infrastructure.Get<IEnumerable<Storage>>(address);

            return storages;
        }
    }
}
