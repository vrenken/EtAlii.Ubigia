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

        public Storage GetLocal()
        {
            return _logicalContext.Storages.GetLocal();
        }

        public IAsyncEnumerable<Storage> GetAll()
        {
            return _logicalContext.Storages.GetAll();
        }

        public Storage Get(string name)
        {
            return _logicalContext.Storages.Get(name);
        }

        public Storage Get(Guid itemId)
        {
            return _logicalContext.Storages.Get(itemId);
        }

        public Storage Update(Guid itemId, Storage item)
        {
            return _logicalContext.Storages.Update(itemId, item);
        }

        public Task<Storage> Add(Storage item)
        {
            return _logicalContext.Storages.Add(item);
        }

        public void Remove(Guid itemId)
        {
            _logicalContext.Storages.Remove(itemId);
        }

        public void Remove(Storage item)
        {
            _logicalContext.Storages.Remove(item);
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