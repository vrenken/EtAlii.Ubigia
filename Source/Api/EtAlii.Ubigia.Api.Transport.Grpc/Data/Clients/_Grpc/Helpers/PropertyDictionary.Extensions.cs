// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using EtAlii.Ubigia.Serialization;
    using Newtonsoft.Json;

    public static class PropertyDictionaryExtension
    {
        public static PropertyDictionary ToLocal(this WireProtocol.PropertyDictionary propertyDictionary, ISerializer serializer)
        {
            using var textReader = new StringReader(propertyDictionary.Data);
            using var jsonReader = new JsonTextReader(textReader);
            return serializer.Deserialize<PropertyDictionary>(jsonReader);
        }

        public static WireProtocol.PropertyDictionary ToWire(this PropertyDictionary propertyDictionary, ISerializer serializer)
        {
            var stringBuilder = new StringBuilder();
            using (var textWriter = new StringWriter(stringBuilder))
            {
                serializer.Serialize(textWriter, propertyDictionary);
            }        
            
            return new WireProtocol.PropertyDictionary
            {
                Data = stringBuilder.ToString()
            };
        }

        public static IEnumerable<WireProtocol.PropertyDictionary> ToWire(this IEnumerable<PropertyDictionary> propertyDictionaries, ISerializer serializer)
        {
            return propertyDictionaries.Select(s => s.ToWire(serializer));
        }
    }
}
