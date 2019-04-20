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
            throw new NotSupportedException("TypeId is not supported: " + typeid);
        }


        private TypeId ToTypeId(object value)
        {
            switch (value)
            {
                case null: return TypeId.None;
                case string _: return TypeId.String;
                case char _: return TypeId.Char;
                case bool _: return TypeId.Boolean;
                case sbyte _: return TypeId.SByte;
                case byte _: return TypeId.Byte;
                case short _: return TypeId.Int16;
                case int _: return TypeId.Int32;
                case long _: return TypeId.Int64;
                case ushort _: return TypeId.UInt16;
                case uint _: return TypeId.UInt32;
                case ulong _: return TypeId.UInt64;
                case float _: return TypeId.Single;
                case double _: return TypeId.Double;
                case decimal _: return TypeId.Decimal;
                case DateTime _: return TypeId.DateTime;
                case TimeSpan _: return TypeId.TimeSpan;
                case Guid _: return TypeId.Guid;
                case Version _: return TypeId.Version;
                default: throw new NotSupportedException("Type is not supported: " + value.GetType().Name);
            }
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
