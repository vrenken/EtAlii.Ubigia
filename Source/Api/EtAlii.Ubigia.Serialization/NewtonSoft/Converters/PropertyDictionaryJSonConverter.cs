// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// A Newtonsoft oriented converter capable to serialize a PropertyDictionary from/to Json.
    /// </summary>
    public sealed class PropertyDictionaryJSonConverter : JsonConverter
    {
        /// <summary>
        /// Check if the specified object type can be converted.
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public override bool CanConvert(Type objectType)
        {
            return typeof(PropertyDictionary) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonToken.None)
            {
                reader.Read();
                if (reader.TokenType != JsonToken.StartObject)
                {
                    throw new JsonException($"JsonToken was of type {reader.TokenType}, only objects are supported");
                }
            }
            else if (reader.TokenType != JsonToken.StartObject)
            {
                throw new JsonException($"JsonToken was of type {reader.TokenType}, only objects are supported");
            }

            if (!reader.Read())
            {
                throw new JsonException($"Unable to load binary payload");
            }
            if (reader.TokenType != JsonToken.PropertyName)
            {
                throw new JsonException("JsonToken was not PropertyName");
            }
            var propertyName = reader.Value!;
            if (!propertyName.Equals("d"))
            {
                throw new JsonException("PropertyName Data was not found");
            }

            var value = reader.ReadAsString();

            reader.Read();
            if (reader.TokenType != JsonToken.EndObject)
            {
                throw new JsonException($"JsonToken was of type {reader.TokenType}, but should be EndObject");

            }

            var data = Convert.FromBase64String(value!);
            using var stream = new MemoryStream(data);
            using var binaryReader = new BinaryReader(stream, Encoding.UTF8);
            var dictionary = PropertyDictionary.Read(binaryReader);
            return dictionary;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            using var stream = new MemoryStream();
            using var binaryWriter = new BinaryWriter(stream, Encoding.UTF8);
            PropertyDictionary.Write(binaryWriter, (PropertyDictionary)value);

            var data = stream.GetBuffer().AsSpan(0, (int)stream.Length);
            var stringValue = Convert.ToBase64String(data);

            writer.WriteStartObject();
            writer.WritePropertyName("d");
            writer.WriteValue(stringValue);
            writer.WriteEndObject();
        }
    }
}
