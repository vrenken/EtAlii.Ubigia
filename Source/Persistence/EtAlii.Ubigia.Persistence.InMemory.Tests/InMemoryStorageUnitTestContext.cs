// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.InMemory.Tests
{
    using EtAlii.Ubigia.Persistence.Tests;
    using EtAlii.xTechnology.Hosting;

    public sealed class InMemoryStorageUnitTestContext : StorageUnitTestContextBase
    {
        public InMemoryStorage Storage { get; private set; }

        public InMemoryStorageUnitTestContext()
        {
            Storage = CreateStorage();

            var folder = Storage.PathBuilder.BaseFolder;
            if (Storage.FolderManager.Exists(folder))
            {
                ((IFolderManager)Storage.FolderManager).Delete(folder);
            }
        }

        protected override void Dispose(bool disposing)
        {
            // Cleanup
            if (disposing)
            {
                Storage = null;
            }
        }

        private InMemoryStorage CreateStorage()
        {
            var configuration = new StorageConfiguration()
                .Use(TestAssembly.StorageName)
                .UseStorageDiagnostics(TestServiceConfiguration.Root)
                .UseInMemoryStorage();

            return (InMemoryStorage)new StorageFactory().Create(configuration);
        }
    }
}
