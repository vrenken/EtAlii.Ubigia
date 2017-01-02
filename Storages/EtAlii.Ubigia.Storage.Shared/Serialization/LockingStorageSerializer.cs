namespace EtAlii.Ubigia.Storage
{
    using System.Collections.Concurrent;
    using EtAlii.Ubigia.Api;

    public class LockingStorageSerializer : IStorageSerializer
    {
        private readonly IStorageSerializer _decoree;
        private readonly ConcurrentDictionary<string, object> _lockObjects = new ConcurrentDictionary<string, object>();

        public LockingStorageSerializer(IStorageSerializer decoree)
        {
            _decoree = decoree;
        }

        public string FileNameFormat { get {return _decoree.FileNameFormat; } }

        public void Serialize<T>(string fileName, T item)
            where T : class
        {
            lock (_lockObjects.GetOrAdd(fileName, s => new object()))
            {
                _decoree.Serialize(fileName, item);

                object lockObject;
                _lockObjects.TryRemove(fileName, out lockObject);
            }
        }


        public void Serialize(string fileName, PropertyDictionary item)
        {
            lock (_lockObjects.GetOrAdd(fileName, s => new object()))
            {
                _decoree.Serialize(fileName, item);

                object lockObject;
                _lockObjects.TryRemove(fileName, out lockObject);
            }
        }

        public T Deserialize<T>(string fileName)
            where T : class
        {
            lock (_lockObjects.GetOrAdd(fileName, s => new object()))
            {
                var result = _decoree.Deserialize<T>(fileName);

                object lockObject;
                _lockObjects.TryRemove(fileName, out lockObject);

                return result;
            }
        }

        public PropertyDictionary Deserialize(string fileName)
        {
            lock (_lockObjects.GetOrAdd(fileName, s => new object()))
            {
                var result = _decoree.Deserialize(fileName);

                object lockObject;
                _lockObjects.TryRemove(fileName, out lockObject);

                return result;
            }
        }
    }
}
