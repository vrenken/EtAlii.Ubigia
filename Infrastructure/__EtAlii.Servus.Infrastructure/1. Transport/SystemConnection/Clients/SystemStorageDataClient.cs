namespace EtAlii.Servus.Infrastructure.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Infrastructure.Functional;

    internal sealed class SystemStorageDataClient : SystemStorageClientBase, IStorageDataClient
    {
        private readonly IInfrastructure _infrastructure;

        public SystemStorageDataClient(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }

        public async Task<Storage> Add(string storageName, string storageAddress)
        {
            var storage = new Storage
            {
                Name = storageName,
                Address = storageAddress,
            };

            storage = _infrastructure.Storages.Add(storage);
            return await Task.FromResult(storage);
        }

        public async Task Remove(Guid storageId)
        {
            await Task.Run(() =>
            {
                _infrastructure.Storages.Remove(storageId);
            });
        }

        public async Task<Storage> Change(Guid storageId, string storageName, string storageAddress)
        {
            var storage = new Storage
            {
                Id = storageId,
                Name = storageName,
                Address = storageAddress,
            };
            storage = _infrastructure.Storages.Update(storageId, storage);
            return await Task.FromResult(storage);
        }

        public async Task<Storage> Get(string storageName)
        {
            var storage = _infrastructure.Storages.Get(storageName);
            return await Task.FromResult(storage);
        }

        public async Task<Storage> Get(Guid storageId)
        {
            var storage = _infrastructure.Storages.Get(storageId);
            return await Task.FromResult(storage);
        }

        public async Task<IEnumerable<Storage>> GetAll()
        {
            var storages = _infrastructure.Storages.GetAll();
            return await Task.FromResult(storages);
        }
    }
}
