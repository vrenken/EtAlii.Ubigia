namespace EtAlii.Ubigia.Persistence.Ntfs.Tests
{
    using System;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.Diagnostics;

    public abstract class NtfsStorageTestBase : IDisposable
    {
        protected TestContentFactory TestContentFactory { get; }
        protected TestContentDefinitionFactory TestContentDefinitionFactory { get; }
        protected TestPropertiesFactory TestPropertiesFactory { get; }

        protected IStorage Storage { get; private set; }

        private readonly string _rootFolder = @"c:\temp\" + Guid.NewGuid() + @"\";

        protected NtfsStorageTestBase()
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

        ~NtfsStorageTestBase()
        {
            Dispose(false);
        }

        private IStorage CreateStorage()
        {
            var configuration = new StorageConfiguration()
                .Use(TestAssembly.StorageName)
                .Use(DiagnosticsConfiguration.Default)
                .UseNtfsStorage(_rootFolder);

            return new StorageFactory().Create(configuration);
        }
    }
}
