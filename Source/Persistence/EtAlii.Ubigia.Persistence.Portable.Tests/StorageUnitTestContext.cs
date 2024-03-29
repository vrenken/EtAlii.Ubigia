﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests;

using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Persistence.Portable;
using PCLStorage;

public class StorageUnitTestContext : FileSystemStorageUnitTestContextBase
{
    public IFolder StorageFolder { get; private set; }

    public override async Task InitializeAsync()
    {
        await base
            .InitializeAsync()
            .ConfigureAwait(false);

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
        var options = new StorageOptions(HostConfiguration)
            .Use(UnitTestSettings.StorageName)
            .UseStorageDiagnostics()
            .UsePortableStorage(StorageFolder);

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
        return new PortableStorageSerializer(itemSerializer, propertiesSerializer, StorageFolder);
    }

    public string GetExpectedDirectoryName(ContainerIdentifier containerIdentifier)
    {
        var paths = containerIdentifier.Paths.Take(containerIdentifier.Paths.Length == 1 ? 1 : containerIdentifier.Paths.Length - 1);
        return string.Join(PortablePath.DirectorySeparatorChar.ToString(), paths);
    }
}
