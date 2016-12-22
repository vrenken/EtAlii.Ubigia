namespace EtAlii.Servus.Storage
{
    using System.IO;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;
    using Newtonsoft.Json.Bson;

    public class InternalBsonItemSerializer : IInternalItemSerializer
    {
        private readonly ISerializer _serializer;

        public string FileNameFormat { get { return _fileNameFormat; } }
        private const string _fileNameFormat = "{0}.bson";

        public InternalBsonItemSerializer(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public void Serialize<T>(Stream stream, T item) where T : class
        {
            using (var writer = new BsonWriter(stream))
            {
                writer.CloseOutput = false;
                _serializer.Serialize(writer, new[] { item });
            }
        }

        public T Deserialize<T>(Stream stream) where T : class
        {
            using (var reader = new BsonReader(stream))
            {
                reader.CloseInput = false;
                reader.ReadRootValueAsArray = true;
                var items = _serializer.Deserialize<T[]>(reader);
                return items[0];
            }
        }
    }
}
