// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Logical;

    internal class StorageRepository :  IStorageRepository
    {
        private readonly ILogicalContext _logicalContext;
        private readonly ILocalStorageInitializer _localStorageInitializer;
        private readonly IStorageInitializer _storageInitializer;
        private readonly ILocalStorageGetter _localStorageGetter;

        public StorageRepository(
            ILogicalContext logicalContext,
            ILocalStorageInitializer localStorageInitializer,
            IStorageInitializer storageInitializer,
            ILocalStorageGetter localStorageGetter)
        {
            _logicalContext = logicalContext;
            _localStorageInitializer = localStorageInitializer;
            _storageInitializer = storageInitializer;
            _localStorageGetter = localStorageGetter;
        }

        /// <inheritdoc />
        public Task<Storage> GetLocal()
        {
            var storage = _localStorageGetter.GetLocal();
            return Task.FromResult(storage);
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
        public async Task<Storage> Add(Storage item)
        {
            var storage = await _logicalContext.Storages
                .Add(item)
                .ConfigureAwait(false);

            if (storage != null)
            {
                await _storageInitializer
                    .Initialize(storage)
                    .ConfigureAwait(false);
            }

            return storage;
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

        public async Task Initialize()
        {
            // Improve this method and use a better way to decide if the local Storage needs to be added.
            // This current test to see if the local storage has already been added is not very stable/scalable.
            // Please find another way to determine that the local storage needs initialization.
            // More details can be found in the Github issue below:
            // https://github.com/vrenken/EtAlii.Ubigia/issues/94

            var localStorage = _localStorageGetter
                .GetLocal();

            var items = await _logicalContext.Storages
                .GetAll()
                .ToArrayAsync()
                .ConfigureAwait(false);

            var isAlreadyRegistered = items.Any(s => s.Name == localStorage.Name);
            if (!isAlreadyRegistered)
            {
                await _logicalContext.Storages
                    .AddLocalStorage(localStorage)
                    .ConfigureAwait(false);

                await _localStorageInitializer
                    .Initialize(localStorage)
                    .ConfigureAwait(false);
            }
        }
    }
}
