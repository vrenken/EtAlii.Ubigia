// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Azure;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AzureFolderManager : IFolderManager
{
    public void SaveToFolder<T>(T item, string itemName, string folder)
        where T : class
    {
        throw new NotImplementedException();
    }

    public Task<T> LoadFromFolder<T>(string folderName, string itemName)
        where T : class
    {
        throw new NotImplementedException();
    }


    public IEnumerable<string> EnumerateFiles(string folderName)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> EnumerateFiles(string folderName, string searchPattern)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> EnumerateDirectories(string folderName)
    {
        throw new NotImplementedException();
    }


    public bool Exists(string folderName)
    {
        throw new NotImplementedException();
    }

    public void Create(string folderName)
    {
        throw new NotImplementedException();
    }

    public void Delete(string folderName)
    {
        throw new NotImplementedException();
    }
}
