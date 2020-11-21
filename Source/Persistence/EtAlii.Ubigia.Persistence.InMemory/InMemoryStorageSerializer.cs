namespace EtAlii.Ubigia.Persistence.InMemory
{
    using System.Threading.Tasks;

    public class InMemoryStorageSerializer : IStorageSerializer
    {
        private readonly IInternalItemSerializer _itemSerializer;
        private readonly IInternalPropertiesSerializer _propertiesSerializer;

        private readonly IInMemoryItemsHelper _inMemoryItemsHelper;

        public string FileNameFormat { get; } = "{0}.bson";

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
            using var stream = _inMemoryItemsHelper.CreateFile(fileName, out var file);
            
            _itemSerializer.Serialize(stream, item);
            file.Content = stream.ToArray();
        }

        public void Serialize(string fileName, PropertyDictionary item)
        {
            using var stream = _inMemoryItemsHelper.CreateFile(fileName, out var file);

            _propertiesSerializer.Serialize(stream, item);
            file.Content = stream.ToArray();
        }

        public async Task<T> Deserialize<T>(string fileName)
            where T : class
        {
            await using var stream = _inMemoryItemsHelper.OpenFile(fileName);
            return await _itemSerializer.Deserialize<T>(stream).ConfigureAwait(false);
        }

        public PropertyDictionary Deserialize(string fileName)
        {
            using var stream = _inMemoryItemsHelper.OpenFile(fileName);
            
            return _propertiesSerializer.Deserialize(stream);
        }
    }
}
