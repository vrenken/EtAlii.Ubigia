namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class StorageDataClientStub : IStorageDataClient
    {
        public Task<Storage> Add(string storageName, string storageAddress)
        {
            return Task.FromResult<Storage>(null);
        }

        public Task Remove(Guid storageId)
        {
            return Task.CompletedTask;
        }

        public Task<Storage> Change(Guid storageId, string storageName, string storageAddress)
        {
            return Task.FromResult<Storage>(null);
        }

        public Task<Storage> Get(string storageName)
        {
            return Task.FromResult<Storage>(null);
        }

        public Task<Storage> Get(Guid storageId)
        {
            return Task.FromResult<Storage>(null);
        }

        public Task<IEnumerable<Storage>> GetAll()
        {
            return Task.FromResult<IEnumerable<Storage>>(null);
        }

        public Task Connect(IStorageConnection storageConnection)
        {
            return Task.CompletedTask;
        }

        public Task Disconnect(IStorageConnection storageConnection)
        {
            return Task.CompletedTask;
        }
    }
}
