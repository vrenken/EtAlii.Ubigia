namespace EtAlii.Ubigia.Persistence.NetCoreApp.Tests
{
    using System;
    using EtAlii.Ubigia.Diagnostics;
    using EtAlii.Ubigia.Tests;

    public abstract class NetCoreAppStorageTestBase : IDisposable
    {
        protected TestContentFactory TestContentFactory { get; }
        protected TestContentDefinitionFactory TestContentDefinitionFactory { get; }
        protected TestPropertiesFactory TestPropertiesFactory { get; }

        protected IStorage Storage { get; private set; }

        private readonly string _rootFolder = @"c:\temp\" + Guid.NewGuid() + @"\";

        protected NetCoreAppStorageTestBase()
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

        ~NetCoreAppStorageTestBase()
        {
            Dispose(false);
        }

        private IStorage CreateStorage()
        {
            var configuration = new StorageConfiguration()
                .Use(TestAssembly.StorageName)
                .Use(UbigiaDiagnostics.DefaultConfiguration)
                .UseNetCoreAppStorage(_rootFolder);

            return new StorageFactory().Create(configuration);
        }
    }
}
