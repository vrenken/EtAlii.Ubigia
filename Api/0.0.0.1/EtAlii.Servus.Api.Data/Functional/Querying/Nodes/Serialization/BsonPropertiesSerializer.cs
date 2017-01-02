using Newtonsoft.Json;

namespace EtAlii.Servus.Api.Data
{
    using Newtonsoft.Json.Bson;
    using System.Collections.Generic;
    using System.IO;

    public class BsonPropertiesSerializer : IPropertiesSerializer
    {
        private readonly JsonSerializer _serializer;

        public BsonPropertiesSerializer(JsonSerializer serializer)
        {
            _serializer = serializer;
        }

        public void Serialize(IDictionary<string, object> properties, Stream stream)
        {
            using (var writer = new BsonWriter(stream))
            {
                writer.CloseOutput = false;
                _serializer.Serialize(writer, properties);
            }
        }

        public IDictionary<string, object> Deserialize<T>(Stream stream)
        {
            IDictionary<string, object> properties;

            using (var reader = new BsonReader(stream))
            {
                reader.CloseInput = false;
                properties = _serializer.Deserialize<IDictionary<string, object>>(reader);
            }
            return properties ?? new Dictionary<string, object>();
        }
    }
}
