// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IImmutableFolderManager
{
    void SaveToFolder<T>(T item, string itemName, string folder)
        where T : class;

    Task<T> LoadFromFolder<T>(string folderName, string itemName)
        where T : class;

    IEnumerable<string> EnumerateFiles(string folderName);
    IEnumerable<string> EnumerateFiles(string folderName, string searchPattern);

    IEnumerable<string> EnumerateDirectories(string folderName);

    bool Exists(string folderName);
    void Create(string folderName);
}
