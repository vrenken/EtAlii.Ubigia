namespace EtAlii.Ubigia.Persistence.NetCoreApp
{
    using System.IO;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Serialization;

    public class NetCoreAppStorageSerializer : IStorageSerializer
    {
        private readonly IInternalItemSerializer _itemSerializer;
        private readonly IInternalPropertiesSerializer _propertiesSerializer;

        public string FileNameFormat { get; } = "{0}.bson";

        public NetCoreAppStorageSerializer(
            IInternalItemSerializer itemSerializer, 
            IInternalPropertiesSerializer propertiesSerializer)
        {
            _itemSerializer = itemSerializer;
            _propertiesSerializer = propertiesSerializer;
        }

        public void Serialize<T>(string fileName, T item)
            where T: class
        {
            using var stream = File.Open(fileName, FileMode.CreateNew, FileAccess.Write);
            
            _itemSerializer.Serialize(stream, item);
        }

        public void Serialize(string fileName, PropertyDictionary item)
        {
            using var stream = File.Open(fileName, FileMode.CreateNew, FileAccess.Write);
            
            _propertiesSerializer.Serialize(stream, item);
        }

        public Task<T> Deserialize<T>(string fileName)
            where T : class
        {
            using var stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            
            return _itemSerializer.Deserialize<T>(stream);
        }

        public PropertyDictionary Deserialize(string fileName)
        {
            using var stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            
            return _propertiesSerializer.Deserialize(stream);
        }
    }
}
