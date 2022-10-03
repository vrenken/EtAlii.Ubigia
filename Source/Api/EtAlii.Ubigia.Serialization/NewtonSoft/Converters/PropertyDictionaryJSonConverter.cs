// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization
{
    using System;
    using System.Diagnostics;
    using Newtonsoft.Json;

    /// <summary>
    /// A Newtonsoft oriented converter capable to serialize a PropertyDictionary from/to Json.
    /// </summary>
    public sealed partial class PropertyDictionaryJSonConverter : JsonConverter
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

            if (existingValue is not PropertyDictionary properties)
            {
                properties = objectType == typeof(PropertyDictionary)
                    ? new PropertyDictionary()
                    : (PropertyDictionary)Activator.CreateInstance(objectType);
            }

            if (reader.TokenType != JsonToken.StartArray)
            {
                CheckedRead(reader);
            }

            if (reader.TokenType == JsonToken.StartArray)
            {
                ReadAsArray(reader, properties, serializer);
            }
            else
            {
                ReadAsDictionary(reader, properties, serializer);
            }

            return properties;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteStartArray();

                var properties = (PropertyDictionary)value;

                foreach (var kvp in properties)
                {
                    writer.WriteStartObject();
                    var typeId = TypeIdConverter.ToTypeId(kvp.Value);
                    writer.WritePropertyName("k"); // key
                    serializer.Serialize(writer, kvp.Key);
                    writer.WritePropertyName("t"); // type
                    serializer.Serialize(writer, typeId);
                    if (typeId != TypeId.None)
                    {
                        writer.WritePropertyName("v"); // value
                        serializer.Serialize(writer, kvp.Value);
                    }
                    writer.WriteEndObject();
                }

                writer.WriteEndArray();
            }
        }


        private void ReadKeyValuePair(JsonReader reader, PropertyDictionary properties, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject)
            {
                throw new JsonSerializationException($"Unexpected JSON token when reading PropertyDictionary. Expected StartObject, got {reader.TokenType}.");
            }

            CheckedRead(reader);

            if (reader.TokenType != JsonToken.PropertyName)
            {
                throw new JsonSerializationException($"Unexpected JSON token when reading PropertyDictionary. Expected PropertyName, got {reader.TokenType}.");
            }

            CheckedRead(reader);

            var key = (string)reader.Value;

            CheckedRead(reader);

            if (reader.TokenType != JsonToken.PropertyName)
            {
                throw new JsonSerializationException($"Unexpected JSON token when reading PropertyDictionary. Expected PropertyName, got {reader.TokenType}.");
            }

            CheckedRead(reader);

            var typeId = serializer.Deserialize<TypeId>(reader);

            object value = null;
            if (typeId != TypeId.None)
            {
                CheckedRead(reader);

                if (reader.TokenType != JsonToken.PropertyName)
                {
                    throw new JsonSerializationException($"Unexpected JSON token when reading PropertyDictionary. Expected PropertyName, got {reader.TokenType}.");
                }

                CheckedRead(reader);

                var objectType = TypeIdConverter.ToType(typeId);
                value = serializer.Deserialize(reader, objectType);
            }

            CheckedRead(reader);

            if (reader.TokenType != JsonToken.EndObject)
            {
                throw new JsonSerializationException($"Unexpected JSON token when reading PropertyDictionary. Expected EndObject, got {reader.TokenType}.");
            }

            properties.Add(key!, value);
        }

        private void CheckedRead(JsonReader reader)
        {
            if (!reader.Read())
            {
                throw new JsonSerializationException("Unexpected end when reading PropertyDictionary.");
            }
        }
    }
}
