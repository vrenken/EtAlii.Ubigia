// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization
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
