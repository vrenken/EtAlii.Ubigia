// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

using System.Threading.Tasks;

/// <summary>
/// An interface to abstract away the storage specific serializers.
/// </summary>
public interface IStorageSerializer
{
    string FileNameFormat { get; }

    void Serialize<T>(string fileName, T item)
        where T : class;

    void Serialize(string fileName, PropertyDictionary item);

    Task<T> Deserialize<T>(string fileName)
        where T : class;

    PropertyDictionary Deserialize(string fileName);
}
