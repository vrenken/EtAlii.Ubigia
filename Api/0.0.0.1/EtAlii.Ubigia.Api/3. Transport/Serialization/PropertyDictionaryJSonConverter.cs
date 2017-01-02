namespace EtAlii.Ubigia.Api.Transport
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

            var properties = existingValue as PropertyDictionary;

            if (properties == null)
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
                ReadAsArray(reader, objectType, properties, serializer);
            }
            else
            {
                ReadAsDictionary(reader, objectType, properties, serializer);
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
                throw new JsonSerializationException(String.Format("Unexpected JSON token when reading PropertyDictionary. Expected StartObject, got {0}.", reader.TokenType));
            }

            CheckedRead(reader);

            if (reader.TokenType != JsonToken.PropertyName)
            {
                throw new JsonSerializationException(String.Format("Unexpected JSON token when reading PropertyDictionary. Expected PropertyName, got {0}.", reader.TokenType));
            }

            CheckedRead(reader);

            var key = (string)reader.Value;

            CheckedRead(reader);

            if (reader.TokenType != JsonToken.PropertyName)
            {
                throw new JsonSerializationException(String.Format("Unexpected JSON token when reading PropertyDictionary. Expected PropertyName, got {0}.", reader.TokenType));
            }

            CheckedRead(reader);

            var typeId = serializer.Deserialize<TypeId>(reader);

            object value = null;
            if (typeId != TypeId.None)
            {
                CheckedRead(reader);

                if (reader.TokenType != JsonToken.PropertyName)
                {
                    throw new JsonSerializationException(String.Format("Unexpected JSON token when reading PropertyDictionary. Expected PropertyName, got {0}.", reader.TokenType));
                }

                CheckedRead(reader);

                var objectType = ToType(typeId);
                value = serializer.Deserialize(reader, objectType);
            }

            CheckedRead(reader);

            if (reader.TokenType != JsonToken.EndObject)
            {
                throw new JsonSerializationException(String.Format("Unexpected JSON token when reading PropertyDictionary. Expected EndObject, got {0}.", reader.TokenType));
            }

            properties.Add(key, value);
        }

        private Type ToType(TypeId typeid)
        {
            switch (typeid)
            {
                case TypeId.String: return typeof(string);
                case TypeId.Char: return typeof(Char);
                case TypeId.Boolean: return typeof(Boolean);
                case TypeId.SByte: return typeof(SByte);
                case TypeId.Byte: return typeof(Byte);
                case TypeId.Int16: return typeof(Int16);
                case TypeId.Int32: return typeof(Int32);
                case TypeId.Int64: return typeof(Int64);
                case TypeId.UInt16: return typeof(UInt16);
                case TypeId.UInt32: return typeof(UInt32);
                case TypeId.UInt64: return typeof(UInt64);
                case TypeId.Single: return typeof(Single);
                case TypeId.Double: return typeof(Double);
                case TypeId.Decimal: return typeof(Decimal);
                case TypeId.DateTime: return typeof(DateTime);
                case TypeId.TimeSpan: return typeof(TimeSpan);
                case TypeId.Guid: return typeof(Guid);
                case TypeId.Version: return typeof(Version);
                case TypeId.None: return null;
            }
            throw new NotSupportedException("TypeId is not supported: " + typeid.ToString());
        }


        private TypeId ToTypeId(object value)
        {
            if (value == null) return TypeId.None;
            if (value is String) return TypeId.String;
            if (value is Char) return TypeId.Char;
            if (value is Boolean) return TypeId.Boolean;
            if (value is SByte) return TypeId.SByte;
            if (value is Byte) return TypeId.Byte;
            if (value is Int16) return TypeId.Int16;
            if (value is Int32) return TypeId.Int32;
            if (value is Int64) return TypeId.Int64;
            if (value is UInt16) return TypeId.UInt16;
            if (value is UInt32) return TypeId.UInt32;
            if (value is UInt64) return TypeId.UInt64;
            if (value is Single) return TypeId.Single;
            if (value is Double) return TypeId.Double;
            if (value is Decimal) return TypeId.Decimal;
            if (value is DateTime) return TypeId.DateTime;
            if (value is TimeSpan) return TypeId.TimeSpan;
            if (value is Guid) return TypeId.Guid;
            if (value is Version) return TypeId.Version;

            throw new NotSupportedException("Type is not supported: " + value.GetType().Name);
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
