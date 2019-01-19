namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class StorageDataClientStub : IStorageDataClient
    {
        public async Task<Storage> Add(string storageName, string storageAddress)
        {
            return await Task.FromResult<Storage>(null);
        }

        public async Task Remove(Guid storageId)
        {
            await Task.CompletedTask;
        }

        public async Task<Storage> Change(Guid storageId, string storageName, string storageAddress)
        {
            return await Task.FromResult<Storage>(null);
        }

        public async Task<Storage> Get(string storageName)
        {
            return await Task.FromResult<Storage>(null);
        }

        public async Task<Storage> Get(Guid storageId)
        {
            return await Task.FromResult<Storage>(null);
        }

        public async Task<IEnumerable<Storage>> GetAll()
        {
            return await Task.FromResult<IEnumerable<Storage>>(null);
        }

        public async Task Connect(IStorageConnection storageConnection)
        {
            await Task.CompletedTask;
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Task.CompletedTask;
        }
    }
}
