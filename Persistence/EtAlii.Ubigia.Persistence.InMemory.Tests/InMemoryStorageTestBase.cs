﻿namespace EtAlii.Ubigia.Persistence.InMemory.Tests
{
    using System;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.Diagnostics;

    public abstract class InMemoryStorageTestBase : IDisposable
    {
        protected InMemoryStorage Storage { get; private set; }

        protected TestContentFactory TestContentFactory { get; }
        protected TestContentDefinitionFactory TestContentDefinitionFactory { get; }
        protected TestPropertiesFactory TestPropertiesFactory { get; }

        protected InMemoryStorageTestBase()
        {
            TestContentFactory = new TestContentFactory();
            TestContentDefinitionFactory = new TestContentDefinitionFactory();
            TestPropertiesFactory = new TestPropertiesFactory();

            Storage = CreateStorage();

            var folder = Storage.PathBuilder.BaseFolder;
            if (Storage.FolderManager.Exists(folder))
            {
                ((IFolderManager)Storage.FolderManager).Delete(folder);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
            if (disposing)
            {
                Storage = null;
            }
        }

        ~InMemoryStorageTestBase()
        {
            Dispose(false);
        }

        private InMemoryStorage CreateStorage()
        {
            var configuration = new StorageConfiguration()
                .Use(TestAssembly.StorageName)
                .Use(DiagnosticsConfiguration.Default)
                .UseInMemoryStorage();

            return (InMemoryStorage)new StorageFactory().Create(configuration);
        }
    }
}
