namespace EtAlii.Ubigia.Persistence
{
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
