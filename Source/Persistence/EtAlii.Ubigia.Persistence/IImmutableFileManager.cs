// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

using System.Threading.Tasks;

public interface IImmutableFileManager
{
    void SaveToFile<T>(string path, T item)
        where T : class;
    void SaveToFile(string path, PropertyDictionary item);

    Task<T> LoadFromFile<T>(string path)
        where T : class;

    PropertyDictionary LoadFromFile(string path);

    bool Exists(string path);
}
