namespace EtAlii.Servus.Storage
{
    using System.IO;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;
    using Newtonsoft.Json.Bson;

    public class InternalBsonPropertiesSerializer : IInternalPropertiesSerializer
    {
        private readonly ISerializer _serializer;

        public string FileNameFormat { get { return _fileNameFormat; } }
        private const string _fileNameFormat = "{0}.bson";

        public InternalBsonPropertiesSerializer(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public void Serialize(Stream stream, PropertyDictionary item)
        {
            using (var writer = new BsonWriter(stream))
            {
                writer.CloseOutput = false;
                _serializer.Serialize(writer, item);
            }
        }

        public PropertyDictionary Deserialize(Stream stream)
        {
            PropertyDictionary item = null;

            using (var reader = new BsonReader(stream))
            {
                reader.CloseInput = false;
                item = _serializer.Deserialize<PropertyDictionary>(reader);
            }
            return item;
        }
    }
}
