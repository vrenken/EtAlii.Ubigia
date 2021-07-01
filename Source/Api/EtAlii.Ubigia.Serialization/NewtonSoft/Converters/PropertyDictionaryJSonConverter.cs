// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization
{
    using System;
    using System.Diagnostics;
    using Newtonsoft.Json;

    /// <summary>
    /// A Newtonsoft oriented converter capable to serialize a PropertyDictionary from/to Json.
    /// </summary>
    public partial class PropertyDictionaryJSonConverter : JsonConverter
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

            if (!(existingValue is PropertyDictionary properties))
            {
                properties = (objectType == typeof(PropertyDictionary))
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
                    var typeId = ToTypeId(kvp.Value);
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

                var objectType = ToType(typeId);
                value = serializer.Deserialize(reader, objectType);
            }

            CheckedRead(reader);

            if (reader.TokenType != JsonToken.EndObject)
            {
                throw new JsonSerializationException($"Unexpected JSON token when reading PropertyDictionary. Expected EndObject, got {reader.TokenType}.");
            }

            properties.Add(key!, value);
        }

        private Type ToType(TypeId typeid)
        {
            return typeid switch
            {
                TypeId.String => typeof(string),
                TypeId.Char => typeof(char),
                TypeId.Boolean => typeof(bool),
                TypeId.SByte => typeof(sbyte),
                TypeId.Byte => typeof(byte),
                TypeId.Int16 => typeof(short),
                TypeId.Int32 => typeof(int),
                TypeId.Int64 => typeof(long),
                TypeId.UInt16 => typeof(ushort),
                TypeId.UInt32 => typeof(uint),
                TypeId.UInt64 => typeof(ulong),
                TypeId.Single => typeof(float),
                TypeId.Double => typeof(double),
                TypeId.Decimal => typeof(decimal),
                TypeId.DateTime => typeof(DateTime),
                TypeId.TimeSpan => typeof(TimeSpan),
                TypeId.Guid => typeof(Guid),
                TypeId.Version => typeof(Version),
                TypeId.None => null,
                _ => throw new NotSupportedException("TypeId is not supported: " + typeid)
            };
        }


        private TypeId ToTypeId(object value)
        {
            return value switch
            {
                null => TypeId.None,
                string _ => TypeId.String,
                char _ => TypeId.Char,
                bool _ => TypeId.Boolean,
                sbyte _ => TypeId.SByte,
                byte _ => TypeId.Byte,
                short _ => TypeId.Int16,
                int _ => TypeId.Int32,
                long _ => TypeId.Int64,
                ushort _ => TypeId.UInt16,
                uint _ => TypeId.UInt32,
                ulong _ => TypeId.UInt64,
                float _ => TypeId.Single,
                double _ => TypeId.Double,
                decimal _ => TypeId.Decimal,
                DateTime _ => TypeId.DateTime,
                TimeSpan _ => TypeId.TimeSpan,
                Guid _ => TypeId.Guid,
                Version _ => TypeId.Version,
                _ => throw new NotSupportedException("Type is not supported: " + value.GetType().Name)
            };
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
