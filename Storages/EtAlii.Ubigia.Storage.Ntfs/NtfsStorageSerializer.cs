namespace EtAlii.Ubigia.Storage.Ntfs
{
    using System.IO;
    using EtAlii.Ubigia.Api;
    using Microsoft.Experimental.IO;

    public partial class NtfsStorageSerializer : IStorageSerializer
    {
        private readonly IInternalItemSerializer _itemSerializer;
        private readonly IInternalPropertiesSerializer _propertiesSerializer;

        public string FileNameFormat => _fileNameFormat;
        private const string _fileNameFormat = "{0}.bson";

        public NtfsStorageSerializer(
            IInternalItemSerializer itemSerializer, 
            IInternalPropertiesSerializer propertiesSerializer)
        {
            _itemSerializer = itemSerializer;
            _propertiesSerializer = propertiesSerializer;
        }

        public void Serialize<T>(string fileName, T item)
            where T: class
        {
            using (var stream = LongPathFile.Open(fileName, FileMode.CreateNew, FileAccess.Write))
            {
                _itemSerializer.Serialize<T>(stream, item);
            }
        }

        public T Deserialize<T>(string fileName)
            where T : class
        {
            using (var stream = LongPathFile.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return _itemSerializer.Deserialize<T>(stream);
            }
        }

        public void Serialize(string fileName, PropertyDictionary item)
        {
            using (var stream = LongPathFile.Open(fileName, FileMode.CreateNew, FileAccess.Write))
            {
                _propertiesSerializer.Serialize(stream, item);
            }
        }

        public PropertyDictionary Deserialize(string fileName)
        {
            using (var stream = LongPathFile.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return _propertiesSerializer.Deserialize(stream);
            }
        }

    }
}
