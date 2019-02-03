namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using Newtonsoft.Json;

    public partial class PropertyDictionaryJSonConverter
    {
        private void ReadAsDictionary(JsonReader reader, PropertyDictionary properties, JsonSerializer serializer)
        {
            while (reader.TokenType == JsonToken.PropertyName)
            {
                CheckedRead(reader);

                ReadKeyValuePair(reader, properties, serializer);

                CheckedRead(reader);
            }
        }
    }
}
