namespace EtAlii.Servus.Api.Logical
{
    using System.IO;
    using EtAlii.Servus.Api.Transport;
    using Newtonsoft.Json.Bson;

    public class BsonPropertiesSerializer : IPropertiesSerializer
    {
        private readonly ISerializer _serializer;

        public BsonPropertiesSerializer(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public void Serialize(PropertyDictionary properties, Stream stream)
        {
            using (var writer = new BsonWriter(stream))
            {
                writer.CloseOutput = false;
                _serializer.Serialize(writer, properties);
            }
        }

        public PropertyDictionary Deserialize(Stream stream)
        {
            PropertyDictionary properties;

            using (var reader = new BsonReader(stream))
            {
                reader.CloseInput = false;
                properties = _serializer.Deserialize<PropertyDictionary>(reader);
            }
            return properties ?? new PropertyDictionary();
        }
    }
}
