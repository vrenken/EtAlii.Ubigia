// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization
{
    using System.IO;
    using Newtonsoft.Json;

    //[Obsolete("JSON based storage has some serious drawbacks. Do not use!")]
    /// <summary>
    /// JSON based storage has some serious drawbacks. Do not use!
    /// </summary>
    public class JsonPropertiesSerializer : IPropertiesSerializer
    {
        private readonly ISerializer _serializer;

        public JsonPropertiesSerializer(ISerializer serializer)
        {
            _serializer = serializer;
            ((Serializer)_serializer).Formatting = Formatting.Indented;
        }

        public void Serialize(Stream stream, PropertyDictionary item)
        {
            using var textWriter = new StreamWriter(stream);
            using var writer = new JsonTextWriter(textWriter);
            
            _serializer.Serialize(writer, item);
        }

        public PropertyDictionary Deserialize(Stream stream)
        {
            using var textReader = new StreamReader(stream);
            using var reader = new JsonTextReader(textReader);
            
            return _serializer.Deserialize<PropertyDictionary>(reader);
        }
    }
}
