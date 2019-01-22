﻿namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api;
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

            _logicalContext.Storages.Initialized += OnLocalStorageInitialized;
            _logicalContext.Storages.Added += OnStorageAdded;
        }

        public Storage GetLocal()
        {
            return _logicalContext.Storages.GetLocal();
        }

        public Storage Get(string name)
        {
            return _logicalContext.Storages.Get(name);
        }

        public IEnumerable<Storage> GetAll()
        {
            return _logicalContext.Storages.GetAll();
        }

        public Storage Get(Guid id)
        {
            return _logicalContext.Storages.Get(id);
        }

        public Storage Update(Guid itemId, Storage item)
        {
            return _logicalContext.Storages.Update(itemId, item);
        }

        public Storage Add(Storage item)
        {
            return _logicalContext.Storages.Add(item);
        }

        public void Remove(Guid itemId)
        {
            _logicalContext.Storages.Remove(itemId);
        }

        public void Remove(Storage itemToRemove)
        {
            _logicalContext.Storages.Remove(itemToRemove);
        }

        private void OnLocalStorageInitialized(object sender, Storage localStorage)
        {
            _localStorageInitializer.Initialize(localStorage);
        }

        private void OnStorageAdded(object sender, Storage storage)
        {
            _storageInitializer.Initialize(storage);
        }
    }
}