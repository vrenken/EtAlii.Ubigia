﻿namespace EtAlii.Ubigia.Persistence
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class DateTimeJSonConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object result = null;
            if (reader.TokenType == JsonToken.Bytes ||
                reader.TokenType == JsonToken.String)
            {
                var bytes = serializer.Deserialize<byte[]>(reader);
                var dateTimeAsLong = BitConverter.ToInt64(bytes, 0);
                result = DateTime.FromBinary(dateTimeAsLong);
            }
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var dateTime = (DateTime) value;
            var dateTimeAsLong = dateTime.ToBinary();
            var bytes = BitConverter.GetBytes(dateTimeAsLong);
            serializer.Serialize(writer, bytes);
        }
    }
}
