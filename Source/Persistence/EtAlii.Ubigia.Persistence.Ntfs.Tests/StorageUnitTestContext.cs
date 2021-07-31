// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System.IO;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence.Ntfs;
    using EtAlii.Ubigia.Serialization;

    public class StorageUnitTestContext : FileSystemStorageUnitTestContextBase
    {
        public override async Task InitializeAsync()
        {
            await base
                .InitializeAsync()
                .ConfigureAwait(false);

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
                .UseStorageDiagnostics(HostConfiguration)
                .UseNtfsStorage(RootFolder);

            return new StorageFactory().Create(configuration);
        }

        public void DeleteFileWhenNeeded(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        public IStorageSerializer CreateSerializer(IItemSerializer itemSerializer, IPropertiesSerializer propertiesSerializer)
        {
            return new NtfsStorageSerializer(itemSerializer, propertiesSerializer);
        }

        public string GetExpectedDirectoryName(ContainerIdentifier containerIdentifier)
        {
            return Path.GetDirectoryName(Path.Combine(containerIdentifier.Paths));
        }
    }
}
