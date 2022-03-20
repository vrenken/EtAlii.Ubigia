// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class StorageContext : StorageClientContextBase<IStorageDataClient>, IStorageContext
    {
        public StorageContext(
            IStorageDataClient data)
            : base(data)
        {
        }
        public async Task<Storage> Add(string storageName, string storageAddress)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            return await Data.Add(storageName, storageAddress).ConfigureAwait(false);
        }

        public async Task Remove(Guid storageId)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            await Data.Remove(storageId).ConfigureAwait(false);
        }

        public async Task<Storage> Change(Guid storageId, string storageName, string storageAddress)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return await Data.Change(storageId, storageName, storageAddress).ConfigureAwait(false);
        }

        public async Task<Storage> Get(string storageName)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return await Data.Get(storageName).ConfigureAwait(false);
        }

        public async Task<Storage> Get(Guid storageId)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return await Data.Get(storageId).ConfigureAwait(false);
        }

        public IAsyncEnumerable<Storage> GetAll()
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return Data.GetAll();
        }
    }
}
