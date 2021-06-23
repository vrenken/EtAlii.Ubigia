// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class StorageDataClientStub : IStorageDataClient
    {
        /// <inheritdoc />
        public Task<Storage> Add(string storageName, string storageAddress)
        {
            return Task.FromResult<Storage>(null);
        }

        /// <inheritdoc />
        public Task Remove(Guid storageId)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<Storage> Change(Guid storageId, string storageName, string storageAddress)
        {
            return Task.FromResult<Storage>(null);
        }

        /// <inheritdoc />
        public Task<Storage> Get(string storageName)
        {
            return Task.FromResult<Storage>(null);
        }

        /// <inheritdoc />
        public Task<Storage> Get(Guid storageId)
        {
            return Task.FromResult<Storage>(null);
        }

        /// <inheritdoc />
        public IAsyncEnumerable<Storage> GetAll()
        {
            return AsyncEnumerable.Empty<Storage>();
        }

        /// <inheritdoc />
        public Task Connect(IStorageConnection storageConnection)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task Disconnect(IStorageConnection storageConnection)
        {
            return Task.CompletedTask;
        }
    }
}
