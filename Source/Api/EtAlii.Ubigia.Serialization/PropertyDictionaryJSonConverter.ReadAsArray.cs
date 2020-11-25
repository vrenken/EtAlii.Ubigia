namespace EtAlii.Ubigia.Serialization
{
    using Newtonsoft.Json;

    public partial class PropertyDictionaryJSonConverter
    {
        private void ReadAsArray(JsonReader reader, PropertyDictionary properties, JsonSerializer serializer) // , Type objectType
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
