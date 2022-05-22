// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Logical;

    internal class StorageRepository :  IStorageRepository
    {
        private readonly ILogicalContext _logicalContext;
        private readonly ILocalStorageInitializer _localStorageInitializer;
        private readonly IStorageInitializer _storageInitializer;

        public StorageRepository(ILogicalContext logicalContext,
            ILocalStorageInitializer localStorageInitializer,
            IStorageInitializer storageInitializer)
        {
            _logicalContext = logicalContext;
            _localStorageInitializer = localStorageInitializer;
            _storageInitializer = storageInitializer;

            _logicalContext.Storages.Initialized = OnLocalStorageInitialized;
            _logicalContext.Storages.Added = OnStorageAdded;
        }

        /// <inheritdoc />
        public Task<Storage> GetLocal()
        {
            return _logicalContext.Storages.GetLocal();
        }

        /// <inheritdoc />
        public IAsyncEnumerable<Storage> GetAll()
        {
            return _logicalContext.Storages.GetAll();
        }

        /// <inheritdoc />
        public Task<Storage> Get(string name)
        {
            return _logicalContext.Storages.Get(name);
        }

        /// <inheritdoc />
        public Task<Storage> Get(Guid itemId)
        {
            return _logicalContext.Storages.Get(itemId);
        }

        /// <inheritdoc />
        public Task<Storage> Update(Guid itemId, Storage item)
        {
            return _logicalContext.Storages.Update(itemId, item);
        }

        /// <inheritdoc />
        public Task<Storage> Add(Storage item)
        {
            return _logicalContext.Storages.Add(item);
        }

        /// <inheritdoc />
        public Task Remove(Guid itemId)
        {
            return _logicalContext.Storages.Remove(itemId);
        }

        /// <inheritdoc />
        public Task Remove(Storage item)
        {
            return _logicalContext.Storages.Remove(item);
        }

        private Task OnLocalStorageInitialized(Storage localStorage)
        {
            return _localStorageInitializer.Initialize(localStorage);
        }

        private Task OnStorageAdded(Storage storage)
        {
            return _storageInitializer.Initialize(storage);
        }
    }
}
