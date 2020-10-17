﻿namespace EtAlii.Ubigia.Persistence.Portable.Tests
{
    using EtAlii.Ubigia.Persistence.Tests;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using PCLStorage;

    public abstract class PortableStorageTestBase : FileSystemStorageTestBase
    {
        protected TestContentFactory TestContentFactory { get; }
        protected TestContentDefinitionFactory TestContentDefinitionFactory { get; }
        protected TestPropertiesFactory TestPropertiesFactory { get; }
        protected IFolder StorageFolder { get; private set; }

        protected PortableStorageTestBase()
        {
            TestContentFactory = new TestContentFactory();
            TestContentDefinitionFactory = new TestContentDefinitionFactory();
            TestPropertiesFactory = new TestPropertiesFactory();
                
            StorageFolder = new FileSystemFolder(RootFolder, false);

            Storage = CreateStorage();

            var folder = Storage.PathBuilder.BaseFolder;
            if (Storage.FolderManager.Exists(folder))
            {
                ((IFolderManager)Storage.FolderManager).Delete(folder);
            }
        }

        private IStorage CreateStorage()
        {
            var configuration = new StorageConfiguration()
                .Use(TestAssembly.StorageName)
                .Use(DiagnosticsConfiguration.Default)
                .UsePortableStorage(StorageFolder);

            return new StorageFactory().Create(configuration);
        }
    }
}
