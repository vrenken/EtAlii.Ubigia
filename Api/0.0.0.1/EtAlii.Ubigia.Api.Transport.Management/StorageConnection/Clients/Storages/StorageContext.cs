namespace EtAlii.Ubigia.Api.Management
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    public sealed class StorageContext : StorageClientContextBase<IStorageDataClient, IStorageNotificationClient>, IStorageContext
    {
        public StorageContext(
            IStorageNotificationClient notifications, 
            IStorageDataClient data) 
            : base(notifications, data)
        {
        }
        public async Task<Storage> Add(string storageName, string storageAddress)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            return await Data.Add(storageName, storageAddress);
        }

        public async Task Remove(Guid storageId)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            await Data.Remove(storageId);
        }

        public async Task<Storage> Change(Guid storageId, string storageName, string storageAddress)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return await Data.Change(storageId, storageName, storageAddress);
        }

        public async Task<Storage> Get(string storageName)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return await Data.Get(storageName);
        }

        public async Task<Storage> Get(Guid storageId)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return await Data.Get(storageId);
        }

        public async Task<IEnumerable<Storage>> GetAll()
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return await Data.GetAll();
        }
    }
}
