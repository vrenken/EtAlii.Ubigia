// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

using System.Threading.Tasks;
using AsyncKeyedLock;

public class LockingStorageSerializer : IStorageSerializer
{
    private readonly IStorageSerializer _decoree;
    private readonly AsyncKeyedLocker<string> _asyncKeyedLocker = new(o =>
    {
        o.PoolSize = 20;
        o.PoolInitialFill = 1;
    });

    public LockingStorageSerializer(IStorageSerializer decoree)
    {
        _decoree = decoree;
        FileNameFormat = _decoree.FileNameFormat;
    }

    public string FileNameFormat { get; }

    public void Serialize<T>(string fileName, T item)
        where T : class
    {
        using (_asyncKeyedLocker.Lock(fileName))
        {
            _decoree.Serialize(fileName, item);
        }
    }


    public void Serialize(string fileName, PropertyDictionary item)
    {
        using (_asyncKeyedLocker.Lock(fileName))
        {
            _decoree.Serialize(fileName, item);
        }
    }

    public async Task<T> Deserialize<T>(string fileName)
        where T : class
    {
        using (await _asyncKeyedLocker.LockAsync(fileName).ConfigureAwait(false))
        {
            var result = await _decoree.Deserialize<T>(fileName).ConfigureAwait(false);
            return result;
        }
    }

    public PropertyDictionary Deserialize(string fileName)
    {
        using (_asyncKeyedLocker.Lock(fileName))
        {
            var result = _decoree.Deserialize(fileName);
            return result;
        }
    }
}
