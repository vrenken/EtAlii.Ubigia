namespace EtAlii.Ubigia.Serialization
{
    using Newtonsoft.Json;

    public partial class PropertyDictionaryJSonConverter
    {
        private void ReadAsDictionary(JsonReader reader, PropertyDictionary properties, JsonSerializer serializer) // , Type objectType
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
