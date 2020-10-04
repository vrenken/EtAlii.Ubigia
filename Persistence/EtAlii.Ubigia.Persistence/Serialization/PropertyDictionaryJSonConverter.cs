﻿namespace EtAlii.Ubigia.Persistence
{
    using System;
    using System.Diagnostics;
    using Newtonsoft.Json;

    public partial class PropertyDictionaryJSonConverter : JsonConverter
    {
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

            // TODO: This if and check statements might not be needed anymore.
            // Needs to be investigated.
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
                throw new JsonSerializationException(
                    $"Unexpected JSON token when reading PropertyDictionary. Expected StartObject, got {reader.TokenType}.");
            }

            CheckedRead(reader);

            if (reader.TokenType != JsonToken.PropertyName)
            {
                throw new JsonSerializationException(
                    $"Unexpected JSON token when reading PropertyDictionary. Expected PropertyName, got {reader.TokenType}.");
            }

            CheckedRead(reader);

            var key = (string)reader.Value;

            CheckedRead(reader);

            if (reader.TokenType != JsonToken.PropertyName)
            {
                throw new JsonSerializationException(
                    $"Unexpected JSON token when reading PropertyDictionary. Expected PropertyName, got {reader.TokenType}.");
            }

            CheckedRead(reader);

            var typeId = serializer.Deserialize<TypeId>(reader);

            object value = null;
            if (typeId != TypeId.None)
            {
                CheckedRead(reader);

                if (reader.TokenType != JsonToken.PropertyName)
                {
                    throw new JsonSerializationException(
                        $"Unexpected JSON token when reading PropertyDictionary. Expected PropertyName, got {reader.TokenType}.");
                }

                CheckedRead(reader);

                var objectType = ToType(typeId);
                value = serializer.Deserialize(reader, objectType);
            }

            CheckedRead(reader);

            if (reader.TokenType != JsonToken.EndObject)
            {
                throw new JsonSerializationException(
                    $"Unexpected JSON token when reading PropertyDictionary. Expected EndObject, got {reader.TokenType}.");
            }

            properties.Add(key, value);
        }

        private Type ToType(TypeId typeid)
        {
            switch (typeid)
            {
                case TypeId.String: return typeof(string);
                case TypeId.Char: return typeof(char);
                case TypeId.Boolean: return typeof(bool);
                case TypeId.SByte: return typeof(sbyte);
                case TypeId.Byte: return typeof(byte);
                case TypeId.Int16: return typeof(short);
                case TypeId.Int32: return typeof(int);
                case TypeId.Int64: return typeof(long);
                case TypeId.UInt16: return typeof(ushort);
                case TypeId.UInt32: return typeof(uint);
                case TypeId.UInt64: return typeof(ulong);
                case TypeId.Single: return typeof(float);
                case TypeId.Double: return typeof(double);
                case TypeId.Decimal: return typeof(decimal);
                case TypeId.DateTime: return typeof(DateTime);
                case TypeId.TimeSpan: return typeof(TimeSpan);
                case TypeId.Guid: return typeof(Guid);
                case TypeId.Version: return typeof(Version);
                case TypeId.None: return null;
            }
            throw new NotSupportedException("TypeId is not supported: " + typeid);
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