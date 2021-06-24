// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization
{
    using System.IO;
    using Newtonsoft.Json.Bson;

    public class BsonPropertiesSerializer : IPropertiesSerializer
    {
        private readonly ISerializer _serializer;

        public BsonPropertiesSerializer(ISerializer serializer)
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
