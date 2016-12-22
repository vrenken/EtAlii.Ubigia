namespace EtAlii.Servus.Storage.Azure
{
    using System;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;

    public partial class AzureStorageSerializer : IStorageSerializer
    {
        private readonly IInternalItemSerializer _itemSerializer;
        private readonly IInternalPropertiesSerializer _propertiesSerializer;

        public string FileNameFormat { get { return _fileNameFormat; } }
        private const string _fileNameFormat = "{0}.bson";

        public AzureStorageSerializer(
            IInternalItemSerializer itemSerializer, 
            IInternalPropertiesSerializer propertiesSerializer)
        {
            _itemSerializer = itemSerializer;
            _propertiesSerializer = propertiesSerializer;
        }

        public void Serialize<T>(string fileName, T item)
            where T: class
        {
            throw new NotImplementedException();
            //using (var stream = LongPathFile.Open(fileName, FileMode.CreateNew, FileAccess.ReadWrite))
            //{
            //    _itemSerializer.Serialize<T>(stream, item);
            //}
        }

        public T Deserialize<T>(string fileName)
            where T : class
        {
            throw new NotImplementedException();
            //using (var stream = LongPathFile.Open(fileName, FileMode.Open, FileAccess.Read))
            //{
            //    return _itemSerializer.Deserialize<T>(stream);
            //}
        }

        public void Serialize(string fileName, PropertyDictionary item)
        {
            throw new NotImplementedException();
            //using (var stream = LongPathFile.Open(fileName, FileMode.CreateNew, FileAccess.ReadWrite))
            //{
            //    _propertiesSerializer.Serialize(stream, item);
            //}
        }

        public PropertyDictionary Deserialize(string fileName)
        {
            throw new NotImplementedException();
            //using (var stream = LongPathFile.Open(fileName, FileMode.Open, FileAccess.Read))
            //{
            //    return _propertiesSerializer.Deserialize(stream);
            //}
        }

    }
}
