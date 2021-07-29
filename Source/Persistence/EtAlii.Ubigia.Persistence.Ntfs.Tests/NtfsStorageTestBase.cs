// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Ntfs.Tests
{
    using EtAlii.Ubigia.Persistence.Tests;
    using EtAlii.xTechnology.Hosting;

    public abstract class NtfsStorageTestBase : FileSystemStorageTestBase
    {
        protected NtfsStorageTestBase()
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
                .UseNtfsStorage(RootFolder);

            return new StorageFactory().Create(configuration);
        }
    }
}
