// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

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

            storage = await _infrastructure.Storages.Add(storage).ConfigureAwait(false);
            return storage;
        }

        public Task Remove(Guid storageId)
        {
            _infrastructure.Storages.Remove(storageId);
            return Task.CompletedTask;
        }

        public Task<Storage> Change(Guid storageId, string storageName, string storageAddress)
        {
            var storage = new Storage
            {
                Id = storageId,
                Name = storageName,
                Address = storageAddress,
            };
            storage = _infrastructure.Storages.Update(storageId, storage);
            return Task.FromResult(storage);
        }

        public Task<Storage> Get(string storageName)
        {
            var storage = _infrastructure.Storages.Get(storageName);
            return Task.FromResult(storage);
        }

        public Task<Storage> Get(Guid storageId)
        {
            var storage = _infrastructure.Storages.Get(storageId);
            return Task.FromResult(storage);
        }

        public IAsyncEnumerable<Storage> GetAll()
        {
            return _infrastructure.Storages.GetAll();
        }
    }
}
