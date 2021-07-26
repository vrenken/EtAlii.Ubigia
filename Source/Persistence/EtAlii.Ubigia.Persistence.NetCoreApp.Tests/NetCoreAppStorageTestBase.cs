// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.NetCoreApp.Tests
{
    using EtAlii.Ubigia.Persistence.Tests;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.Hosting;

    public abstract class NetCoreAppStorageTestBase : FileSystemStorageTestBase
    {
        protected TestContentFactory TestContentFactory { get; }
        protected TestContentDefinitionFactory TestContentDefinitionFactory { get; }
        protected TestPropertiesFactory TestPropertiesFactory { get; }

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

        private IStorage CreateStorage()
        {
            var configuration = new StorageConfiguration()
                .Use(TestAssembly.StorageName)
                .UseStorageDiagnostics(TestConfiguration.Root)
                .UseNetCoreAppStorage(RootFolder);

            return new StorageFactory().Create(configuration);
        }
    }
}
