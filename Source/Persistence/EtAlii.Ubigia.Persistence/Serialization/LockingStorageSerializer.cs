// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    public class LockingStorageSerializer : IStorageSerializer
    {
        private readonly IStorageSerializer _decoree;
        private readonly ConcurrentDictionary<string, SemaphoreSlim> _lockObjects = new();

        public LockingStorageSerializer(IStorageSerializer decoree)
        {
            _decoree = decoree;
            FileNameFormat = _decoree.FileNameFormat;
        }

        public string FileNameFormat { get; }

        public void Serialize<T>(string fileName, T item)
            where T : class
        {
            var lockObject = _lockObjects.GetOrAdd(fileName, _ => new SemaphoreSlim(1, 1));
            lockObject.Wait();
            try
            {
                _decoree.Serialize(fileName, item);
            }
            finally
            {
                _lockObjects.TryRemove(fileName, out _);
                lockObject.Release();
            }
        }


        public void Serialize(string fileName, PropertyDictionary item)
        {
            var lockObject = _lockObjects.GetOrAdd(fileName, _ => new SemaphoreSlim(1, 1));
            lockObject.Wait();
            try
            {
                _decoree.Serialize(fileName, item);
            }
            finally
            {
                _lockObjects.TryRemove(fileName, out _);
                lockObject.Release();
            }
        }

        public async Task<T> Deserialize<T>(string fileName)
            where T : class
        {
            var lockObject = _lockObjects.GetOrAdd(fileName, _ => new SemaphoreSlim(1, 1));
            await lockObject.WaitAsync().ConfigureAwait(false);
            try
            {
                var result = await _decoree.Deserialize<T>(fileName).ConfigureAwait(false);
                return result;
            }
            finally
            {
                _lockObjects.TryRemove(fileName, out _);
                lockObject.Release();
            }
        }

        public PropertyDictionary Deserialize(string fileName)
        {
            var lockObject = _lockObjects.GetOrAdd(fileName, _ => new SemaphoreSlim(1, 1));
            lockObject.Wait();
            try
            {
                var result = _decoree.Deserialize(fileName);
                return result;
            }
            finally
            {
                _lockObjects.TryRemove(fileName, out _);
                lockObject.Release();
            }
        }
    }
}
