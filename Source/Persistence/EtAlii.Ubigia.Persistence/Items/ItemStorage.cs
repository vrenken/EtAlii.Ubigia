// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

using System;
using System.Linq;
using System.Threading.Tasks;

internal class ItemStorage : IItemStorage
{
    private readonly IFileManager _fileManager;
    private readonly IFolderManager _folderManager;
    private readonly IPathBuilder _pathBuilder;
    private readonly IStorageSerializer _serializer;

    public ItemStorage(IStorageSerializer serializer, IFileManager fileManager, IFolderManager folderManager, IPathBuilder pathBuilder)
    {
        _serializer = serializer;
        _pathBuilder = pathBuilder;
        _fileManager = fileManager;
        _folderManager = folderManager;
    }

    public void Remove(Guid id, ContainerIdentifier container)
    {
        try
        {
            var idName = id.ToString();
            var fileName = _pathBuilder.GetFileName(idName, container);

            // Ensure that the requested folder exists.
            var folder = _pathBuilder.GetDirectoryName(fileName);
            _folderManager.Create(folder);

            _fileManager.Delete(fileName);
        }
        catch (Exception e)
        {
            throw new StorageException("Unable to remove item from the specified container", e);
        }
    }

    public void Store<T>(T item, Guid id, ContainerIdentifier container)
        where T : class
    {
        try
        {
            var idName = id.ToString();
            var fileName = _pathBuilder.GetFileName(idName, container);
            _fileManager.SaveToFile(fileName, item);
        }
        catch (Exception e)
        {
            throw new StorageException("Unable to store item in the specified container", e);
        }
    }

    public bool Has(Guid id, ContainerIdentifier container)
    {
        try
        {
            var idName = id.ToString();
            var fileName = _pathBuilder.GetFileName(idName, container);
            return _fileManager.Exists(fileName);
        }
        catch (Exception e)
        {
            throw new StorageException("Unable to check for item in the specified container", e);
        }
    }

    public async Task<T> Retrieve<T>(Guid id, ContainerIdentifier container)
        where T : class
    {
        try
        {
            var idName = id.ToString();
            var fileName = _pathBuilder.GetFileName(idName, container);
            return await _fileManager.LoadFromFile<T>(fileName).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            throw new StorageException("Unable to retrieve item from the specified container", e);
        }
    }

    public Guid[] Get(ContainerIdentifier container)
    {
        try
        {
            var folder = _pathBuilder.GetFolder(container);
            // Ensure that the requested folder exists.
            _folderManager.Create(folder);

            var searchPattern = string.Format(_serializer.FileNameFormat, "*");
            var fileNames = _folderManager.EnumerateFiles(folder, searchPattern)
                .Select(fileName => _pathBuilder.GetFileNameWithoutExtension(fileName))
                .ToArray();

            return fileNames.Select(Guid.Parse)
                .ToArray();
        }
        catch (Exception e)
        {
            throw new StorageException("Unable to get item from the specified container", e);
        }
    }
}
