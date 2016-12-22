﻿namespace EtAlii.Servus.Storage
{
    using System.IO;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;
    using Newtonsoft.Json;

    //[Obsolete("JSON based storage has some serious drawbacks. Do not use!")]
    /// <summary>
    /// JSON based storage has some serious drawbacks. Do not use!
    /// </summary>
    public class InternalJsonPropertiesSerializer : IInternalPropertiesSerializer
    {
        private readonly ISerializer _serializer;

        public string FileNameFormat { get { return _fileNameFormat; } }
        private const string _fileNameFormat = "{0}.json";

        public InternalJsonPropertiesSerializer(ISerializer serializer)
        {
            _serializer = serializer;
            _serializer.Formatting = Formatting.Indented;
        }

        public void Serialize(Stream stream, PropertyDictionary item)
        {
            using (var textWriter = new StreamWriter(stream))
            {
                using (var writer = new JsonTextWriter(textWriter))
                {
                    _serializer.Serialize(writer, item);
                }
            }
        }

        public PropertyDictionary Deserialize(Stream stream)
        {
            using (var textReader = new StreamReader(stream))
            {
                //var result = textReader.ReadToEnd();
                using (var reader = new JsonTextReader(textReader))
                {
                    return _serializer.Deserialize<PropertyDictionary>(reader);
                }
            }
        }
    }
}
