﻿namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Diagnostics;
    using Newtonsoft.Json;

    public class UnsignedLongJSonConverter : JsonConverter
    {
        [DebuggerStepThrough]
        public override bool CanConvert(Type objectType)
        {
            return typeof(ulong) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            ulong result = 0;
            if (reader.TokenType == JsonToken.Bytes || 
                reader.TokenType == JsonToken.String)
            {
                var bytes = serializer.Deserialize<byte[]>(reader);
                //var bytes = reader.ReadAsBytes()
                result = BitConverter.ToUInt64(bytes, 0);
            }
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if(value == null)
            {
                writer.WriteNull();
            }
            else
            {
                var bytes = BitConverter.GetBytes((ulong)value);
                serializer.Serialize(writer, bytes);
                //writer.WriteValue(bytes)
            }
        }
    }
}
