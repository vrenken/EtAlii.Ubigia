// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization
{
    using Newtonsoft.Json;

    public partial class PropertyDictionaryJSonConverter
    {
        private void ReadAsArray(JsonReader reader, PropertyDictionary properties, JsonSerializer serializer)
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
