namespace EtAlii.Ubigia.Persistence
{
    using System.Collections.Concurrent;

    public class LockingStorageSerializer : IStorageSerializer
    {
        private readonly IStorageSerializer _decoree;
        private readonly ConcurrentDictionary<string, object> _lockObjects = new ConcurrentDictionary<string, object>();

        public LockingStorageSerializer(IStorageSerializer decoree)
        {
            _decoree = decoree;
            FileNameFormat = _decoree.FileNameFormat;
        }

        public string FileNameFormat { get; }

        public void Serialize<T>(string fileName, T item)
            where T : class
        {
            lock (_lockObjects.GetOrAdd(fileName, _ => new object()))
            {
                _decoree.Serialize(fileName, item);

                _lockObjects.TryRemove(fileName, out _);
            }
        }


        public void Serialize(string fileName, PropertyDictionary item)
        {
            lock (_lockObjects.GetOrAdd(fileName, _ => new object()))
            {
                _decoree.Serialize(fileName, item);

                _lockObjects.TryRemove(fileName, out _);
            }
        }

        public T Deserialize<T>(string fileName)
            where T : class
        {
            lock (_lockObjects.GetOrAdd(fileName, _ => new object()))
            {
                var result = _decoree.Deserialize<T>(fileName);

                _lockObjects.TryRemove(fileName, out _);

                return result;
            }
        }

        public PropertyDictionary Deserialize(string fileName)
        {
            lock (_lockObjects.GetOrAdd(fileName, _ => new object()))
            {
                var result = _decoree.Deserialize(fileName);

                _lockObjects.TryRemove(fileName, out _);

                return result;
            }
        }
    }
}
