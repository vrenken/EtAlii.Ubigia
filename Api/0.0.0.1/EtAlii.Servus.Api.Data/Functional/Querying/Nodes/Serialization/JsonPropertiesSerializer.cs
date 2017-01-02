namespace EtAlii.Servus.Api.Data
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class JsonPropertiesSerializer : IPropertiesSerializer
    {
        private readonly Newtonsoft.Json.JsonSerializer _serializer;
        private const int _bufferSize = 4096;

        public JsonPropertiesSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            _serializer = serializer;
            _serializer.Formatting = Formatting.Indented;
        }

        public void Serialize(IDictionary<string, object> properties, Stream stream)
        {
            using (var textWriter = new StreamWriter(stream, Encoding.Unicode, _bufferSize, true))
            {
                using (var writer = new Newtonsoft.Json.JsonTextWriter(textWriter))
                {
                    //writer.CloseOutput = false;
                    _serializer.Serialize(writer, properties);
                }
            }
        }

        public IDictionary<string, object> Deserialize<T>(Stream stream)
        {
            IDictionary<string, object> properties = null;

            using (var textReader = new StreamReader(stream))
            {
                using (var reader = new Newtonsoft.Json.JsonTextReader(textReader))
                {
                    properties = _serializer.Deserialize<IDictionary<string, object>>(reader);
                }
            }
            return properties ?? new Dictionary<string, object>();
        }
    }
}
