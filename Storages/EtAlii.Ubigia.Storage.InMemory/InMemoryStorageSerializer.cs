namespace EtAlii.Ubigia.Storage
{
    using EtAlii.Ubigia.Api;

    public class InMemoryStorageSerializer : IStorageSerializer
    {
        private readonly IInternalItemSerializer _itemSerializer;
        private readonly IInternalPropertiesSerializer _propertiesSerializer;

        private readonly IInMemoryItemsHelper _inMemoryItemsHelper;

        public string FileNameFormat => _fileNameFormat;
        private const string _fileNameFormat = "{0}.bson";

        public InMemoryStorageSerializer(
            IInternalItemSerializer itemSerializer, 
            IInternalPropertiesSerializer propertiesSerializer,
            IInMemoryItemsHelper inMemoryItemsHelper)
        {
            _itemSerializer = itemSerializer;
            _propertiesSerializer = propertiesSerializer;
            _inMemoryItemsHelper = inMemoryItemsHelper;
        }

        public void Serialize<T>(string fileName, T item)
            where T : class
        {
            File file;
            var stream = _inMemoryItemsHelper.CreateFile(fileName, out file);
            {
                _itemSerializer.Serialize<T>(stream, item);
            }
            file.Content = stream.ToArray();
        }

        public T Deserialize<T>(string fileName)
            where T : class
        {
            using (var stream = _inMemoryItemsHelper.OpenFile(fileName))
            {
                return _itemSerializer.Deserialize<T>(stream);
            }
        }

        public void Serialize(string fileName, PropertyDictionary item)
        {
            File file;
            var stream = _inMemoryItemsHelper.CreateFile(fileName, out file);
            {
                _propertiesSerializer.Serialize(stream, item);
            }
            file.Content = stream.ToArray();
        }

        public PropertyDictionary Deserialize(string fileName)
        {
            using (var stream = _inMemoryItemsHelper.OpenFile(fileName))
            {
                return _propertiesSerializer.Deserialize(stream);
            }
        }

    }
}
