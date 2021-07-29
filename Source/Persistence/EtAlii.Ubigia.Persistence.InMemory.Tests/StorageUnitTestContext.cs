// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System.IO;
    using EtAlii.Ubigia.Persistence.InMemory;
    using EtAlii.Ubigia.Serialization;
    using EtAlii.xTechnology.Hosting;

    public class StorageUnitTestContext : StorageUnitTestContextBase
    {
        public InMemoryStorage Storage { get; private set; }

        public StorageUnitTestContext()
        {
            Storage = CreateStorage();

            var folder = Storage.PathBuilder.BaseFolder;
            if (Storage.FolderManager.Exists(folder))
            {
                ((IFolderManager)Storage.FolderManager).Delete(folder);
            }
        }

        protected override void Dispose(bool disposing)
        {
            // Cleanup
            if (disposing)
            {
                Storage = null;
            }
        }

        private InMemoryStorage CreateStorage()
        {
            var configuration = new StorageConfiguration()
                .Use(TestAssembly.StorageName)
                .UseStorageDiagnostics(TestServiceConfiguration.Root)
                .UseInMemoryStorage();

            return (InMemoryStorage)new StorageFactory().Create(configuration);
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
