﻿namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Threading.Tasks;

    internal class StorageInitializer : IStorageInitializer
    {
        public Task Initialize(Storage storage)
        {
            // Initialize the specified storage.
            return Task.CompletedTask;
        }
    }
}