namespace EtAlii.Ubigia.Persistence
{
    using System.IO;
    using Newtonsoft.Json.Bson;

    public class InternalBsonPropertiesSerializer : IInternalPropertiesSerializer
    {
        private readonly ISerializer _serializer;

        public string FileNameFormat { get; } = "{0}.bson";

        public InternalBsonPropertiesSerializer(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public void Serialize(Stream stream, PropertyDictionary item)
        {
            using var writer = new BsonDataWriter(stream) {CloseOutput = false};

            _serializer.Serialize(writer, item);
        }

        public PropertyDictionary Deserialize(Stream stream)
        {
            using var reader = new BsonDataReader(stream) {CloseInput = false};

            return _serializer.Deserialize<PropertyDictionary>(reader);
        }
    }
}
