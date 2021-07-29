// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Portable.Tests
{
    using EtAlii.Ubigia.Persistence.Tests;
    using EtAlii.xTechnology.Hosting;
    using PCLStorage;

    public abstract class PortableStorageTestBase : FileSystemStorageTestBase
    {
        protected IFolder StorageFolder { get; }

        protected PortableStorageTestBase()
        {
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
                .UseStorageDiagnostics(TestServiceConfiguration.Root)
                .UsePortableStorage(StorageFolder);

            return new StorageFactory().Create(configuration);
        }
    }
}
