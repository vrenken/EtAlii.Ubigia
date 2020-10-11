namespace EtAlii.Ubigia.Persistence.Portable.Tests
{
    using System;
    using System.IO;
    using EtAlii.Ubigia.Diagnostics;
    using EtAlii.Ubigia.Tests;
    using PCLStorage;

    public abstract class PortableStorageTestBase : IDisposable
    {
        protected TestContentFactory TestContentFactory { get; }
        protected TestContentDefinitionFactory TestContentDefinitionFactory { get; }
        protected TestPropertiesFactory TestPropertiesFactory { get; }

        protected IStorage Storage { get; private set; }

        protected IFolder StorageFolder { get; private set; }

        private readonly string _rootFolder;
        private static int _testIndex; 


        protected PortableStorageTestBase()
        {
            TestContentFactory = new TestContentFactory();
            TestContentDefinitionFactory = new TestContentDefinitionFactory();
            TestPropertiesFactory = new TestPropertiesFactory();

            _rootFolder = @"c:\temp\" + _testIndex;
            _testIndex += 1;
            _testIndex = _testIndex > 999 ? 0 : _testIndex;

            if (Directory.Exists(_rootFolder))
            {
                Directory.Delete(_rootFolder, true);
            }
            Directory.CreateDirectory(_rootFolder);

            StorageFolder = new FileSystemFolder(_rootFolder, false);

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
                StorageFolder = null;
                Storage = null;

                if (Directory.Exists(_rootFolder))
                {
                    Directory.Delete(_rootFolder, true);
                }
            }
        }

        ~PortableStorageTestBase()
        {
            Dispose(false);
        }

        private IStorage CreateStorage()
        {
            var configuration = new StorageConfiguration()
                .Use(TestAssembly.StorageName)
                .Use(UbigiaDiagnostics.DefaultConfiguration)
                .UsePortableStorage(StorageFolder);

            return new StorageFactory().Create(configuration);
        }
    }
}
