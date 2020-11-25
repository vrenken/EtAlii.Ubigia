namespace EtAlii.Ubigia.Serialization
{
    using System.IO;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    //[Obsolete("JSON based storage has some serious drawbacks. Do not use!")]
    /// <summary>
    /// JSON based storage has some serious drawbacks. Do not use!
    /// </summary>
    public class InternalJsonItemSerializer : IInternalItemSerializer
    {
        private readonly ISerializer _serializer;

        public string FileNameFormat { get; } = "{0}.json";

        public InternalJsonItemSerializer(ISerializer serializer)
        {
            _serializer = serializer;
            _serializer.Formatting = Formatting.Indented;
        }

        public void Serialize<T>(Stream stream, T item) where T : class
        {
            using var textWriter = new StreamWriter(stream);
            using var writer = new JsonTextWriter(textWriter);
            
            _serializer.Serialize(writer, item);
        }

        public Task<T> Deserialize<T>(Stream stream) where T : class
        {
            using var textReader = new StreamReader(stream);
            using var reader = new JsonTextReader(textReader);
            
            var item = _serializer.Deserialize<T>(reader);
            return Task.FromResult(item);
        }
    }
}
