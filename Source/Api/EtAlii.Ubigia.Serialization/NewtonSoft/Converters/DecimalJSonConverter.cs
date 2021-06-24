// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using Newtonsoft.Json;

    public class DecimalJSonConverter : JsonConverter
    {
        [DebuggerStepThrough]
        public override bool CanConvert(Type objectType)
        {
            return typeof(decimal) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            decimal result = 0;
            if (reader.TokenType == JsonToken.String)
            {
                var text = serializer.Deserialize<string>(reader);
                result = Convert.ToDecimal(text, CultureInfo.InvariantCulture);
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
                var text = Convert.ToString((decimal)value, CultureInfo.InvariantCulture);
                serializer.Serialize(writer, text);
            }
        }
    }
}
