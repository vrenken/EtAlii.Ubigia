namespace EtAlii.Servus.Api.Logical
{
    using System.IO;
    using System.Text;
    using EtAlii.Servus.Api.Transport;
    using Newtonsoft.Json;

    //[Obsolete("JSON based storage has some serious drawbacks. Do not use!")]
    /// <summary>
    /// JSON based storage has some serious drawbacks. Do not use!
    /// </summary>
    public class JsonPropertiesSerializer : IPropertiesSerializer
    {
        private readonly ISerializer _serializer;
        private const int _bufferSize = 4096;

        public JsonPropertiesSerializer(ISerializer serializer)
        {
            _serializer = serializer;
            _serializer.Formatting = Formatting.Indented;
        }

        public void Serialize(PropertyDictionary properties, Stream stream)
        {
            using (var textWriter = new StreamWriter(stream, Encoding.Unicode, _bufferSize, true))
            {
                using (var writer = new JsonTextWriter(textWriter))
                {
                    //writer.CloseOutput = false;
                    _serializer.Serialize(writer, properties);
                }
            }
        }

        public PropertyDictionary Deserialize(Stream stream)
        {
            PropertyDictionary properties = null;

            using (var textReader = new StreamReader(stream))
            {
                using (var reader = new JsonTextReader(textReader))
                {
                    properties = _serializer.Deserialize<PropertyDictionary>(reader);
                }
            }
            return properties ?? new PropertyDictionary();
        }
    }
}
