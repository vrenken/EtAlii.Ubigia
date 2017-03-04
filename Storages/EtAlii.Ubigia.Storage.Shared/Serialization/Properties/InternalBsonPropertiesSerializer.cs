namespace EtAlii.Ubigia.Storage
{
    using System.IO;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using Newtonsoft.Json.Bson;

    public class InternalBsonPropertiesSerializer : IInternalPropertiesSerializer
    {
        private readonly ISerializer _serializer;

        public string FileNameFormat => _fileNameFormat;
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
