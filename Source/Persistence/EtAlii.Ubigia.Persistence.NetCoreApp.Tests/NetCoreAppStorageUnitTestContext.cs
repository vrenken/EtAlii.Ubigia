// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.NetCoreApp.Tests
{
    using EtAlii.Ubigia.Persistence.Tests;
    using EtAlii.xTechnology.Hosting;

    public class NetCoreAppStorageUnitTestContext : FileSystemStorageUnitTestContextBase
    {
        public NetCoreAppStorageUnitTestContext()
        {
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
                .UseStorageDiagnostics(TestServiceConfiguration.Root)
                .UseNetCoreAppStorage(RootFolder);

            return new StorageFactory().Create(configuration);
        }
    }
}
