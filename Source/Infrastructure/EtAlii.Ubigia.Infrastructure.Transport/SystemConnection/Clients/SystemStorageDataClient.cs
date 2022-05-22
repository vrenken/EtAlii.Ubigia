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

        /// <inheritdoc />
        public Task<Storage> Add(string storageName, string storageAddress)
        {
            var storage = new Storage
            {
                Name = storageName,
                Address = storageAddress,
            };

            return _infrastructure.Storages.Add(storage);
        }

        /// <inheritdoc />
        public Task Remove(Guid storageId)
        {
            return _infrastructure.Storages.Remove(storageId);
        }

        /// <inheritdoc />
        public Task<Storage> Change(Guid storageId, string storageName, string storageAddress)
        {
            var storage = new Storage
            {
                Id = storageId,
                Name = storageName,
                Address = storageAddress,
            };
            return _infrastructure.Storages.Update(storageId, storage);
        }

        /// <inheritdoc />
        public Task<Storage> Get(string storageName)
        {
            return _infrastructure.Storages.Get(storageName);
        }

        /// <inheritdoc />
        public Task<Storage> Get(Guid storageId)
        {
            return _infrastructure.Storages.Get(storageId);
        }

        /// <inheritdoc />
        public IAsyncEnumerable<Storage> GetAll()
        {
            return _infrastructure.Storages.GetAll();
        }
    }
}
