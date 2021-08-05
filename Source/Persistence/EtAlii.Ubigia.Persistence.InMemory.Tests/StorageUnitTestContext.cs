// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System.IO;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence.InMemory;
    using EtAlii.Ubigia.Serialization;

    public class StorageUnitTestContext : StorageUnitTestContextBase
    {
        public InMemoryStorage Storage { get; private set; }

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

        public override async Task DisposeAsync()
        {
            await base
                .DisposeAsync()
                .ConfigureAwait(false);

            // Cleanup
            Storage = null;
        }

        private InMemoryStorage CreateStorage()
        {
            var options = new StorageOptions(HostConfiguration)
                .Use(TestAssembly.StorageName)
                .UseStorageDiagnostics()
                .UseInMemoryStorage();

            return (InMemoryStorage)new StorageFactory().Create(options);
        }

        public void DeleteFileWhenNeeded(string fileName)
        {
            if (Storage.InMemoryItems.Exists(fileName))
            {
                Storage.InMemoryItems.Delete(fileName);
            }
        }

        public IStorageSerializer CreateSerializer(IItemSerializer itemSerializer, IPropertiesSerializer propertiesSerializer)
        {
            return new InMemoryStorageSerializer(itemSerializer, propertiesSerializer, Storage.InMemoryItemsHelper);
        }

        public string GetExpectedDirectoryName(ContainerIdentifier containerIdentifier)
        {
            return Path.GetDirectoryName(Path.Combine(containerIdentifier.Paths));
        }
    }
}
