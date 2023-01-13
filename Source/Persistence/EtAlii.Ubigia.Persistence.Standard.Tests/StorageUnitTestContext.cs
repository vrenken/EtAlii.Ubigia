// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests;

using System.IO;
using System.Threading.Tasks;
using EtAlii.Ubigia.Persistence.Standard;

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
        var options = new StorageOptions(HostConfiguration)
            .Use(UnitTestSettings.StorageName)
            .UseStorageDiagnostics()
            .UseStandardStorage(RootFolder);

        return new StorageFactory().Create(options);
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
        return new StandardStorageSerializer(itemSerializer, propertiesSerializer);
    }

    public string GetExpectedDirectoryName(ContainerIdentifier containerIdentifier)
    {
        return Path.GetDirectoryName(Path.Combine(containerIdentifier.Paths));
    }
}
