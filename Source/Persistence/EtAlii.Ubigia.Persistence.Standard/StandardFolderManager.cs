// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Standard;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

internal class StandardFolderManager : IFolderManager
{
    private readonly IStorageSerializer _serializer;

    public StandardFolderManager(IStorageSerializer serializer)
    {
        _serializer = serializer;
    }

    public void SaveToFolder<T>(T item, string itemName, string folder)
        where T : class
    {
        if (!Directory.Exists(folder))
        {
            throw new InvalidOperationException($"The provided entry has not been prepared by the {nameof(StandardFolderManager)}.");
        }

        var fileName = string.Format(_serializer.FileNameFormat, itemName);
        fileName = Path.Combine(folder, fileName);

        _serializer.Serialize(fileName, item);

        if (GetLength(fileName) == 0)
        {
            throw new InvalidOperationException("An empty file has been stored.");
        }
    }

    private long GetLength(string fileName)
    {
        var fileInfo = new FileInfo(fileName);
        return fileInfo.Length;
    }

    public async Task<T> LoadFromFolder<T>(string folderName, string itemName)
        where T : class
    {
        T item = null;

        if (Directory.Exists(folderName))
        {
            var fileName = string.Format(_serializer.FileNameFormat, itemName);
            fileName = Path.Combine(folderName, fileName);

            if (File.Exists(fileName))
            {
                item = await _serializer.Deserialize<T>(fileName).ConfigureAwait(false);
            }
        }
        return item;
    }


    public IEnumerable<string> EnumerateFiles(string folderName)
    {
        return Directory.EnumerateFiles(folderName);
    }

    public IEnumerable<string> EnumerateFiles(string folderName, string searchPattern)
    {
        return Directory.EnumerateFiles(folderName, searchPattern);
    }

    public IEnumerable<string> EnumerateDirectories(string folderName)
    {
        return Directory.EnumerateDirectories(folderName);
    }


    public bool Exists(string folderName)
    {
        return Directory.Exists(folderName);
    }

    public void Create(string folderName)
    {
        var parentFolder = Path.GetDirectoryName(folderName);
        if (!Directory.Exists(parentFolder))
        {
            Create(parentFolder);
        }
        Directory.CreateDirectory(folderName);
    }

    public void Delete(string folderName)
    {
        var subFolders = Directory.EnumerateDirectories(folderName);
        foreach (var subFolder in subFolders)
        {
            Delete(subFolder);
        }

        var files = Directory.EnumerateFiles(folderName);
        foreach (var file in files)
        {
            File.Delete(file);
        }

        Directory.Delete(folderName);
    }
}
