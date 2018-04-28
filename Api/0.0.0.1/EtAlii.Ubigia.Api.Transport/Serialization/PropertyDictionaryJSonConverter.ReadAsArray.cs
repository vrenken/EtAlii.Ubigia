namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using Newtonsoft.Json;

    public partial class PropertyDictionaryJSonConverter
    {
        private void ReadAsArray(JsonReader reader, Type objectType, PropertyDictionary properties, JsonSerializer serializer)
        {
            CheckedRead(reader);

            while (reader.TokenType != JsonToken.EndArray)
            {
                ReadKeyValuePair(reader, properties, serializer);

                CheckedRead(reader);
            }
        }
    }
}
