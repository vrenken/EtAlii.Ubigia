﻿namespace EtAlii.Ubigia.Serialization
{
    using System.IO;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Bson;

    public class InternalBsonItemSerializer : IInternalItemSerializer
    {
        private readonly ISerializer _serializer;

        public string FileNameFormat { get; } = "{0}.bson";

        public InternalBsonItemSerializer(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public void Serialize<T>(Stream stream, T item) where T : class
        {
            using var writer = new BsonDataWriter(stream) {CloseOutput = false};
            
            _serializer.Serialize(writer, new[] { item });
        }

        public Task<T> Deserialize<T>(Stream stream) where T : class
        {
            using var reader = new BsonDataReader(stream) {CloseInput = false, ReadRootValueAsArray = true};
            
            var items = _serializer.Deserialize<T[]>(reader);
            return Task.FromResult(items[0]);
        }
    }
}
