namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    public static class PropertyDictionaryExtension
    {
        private static readonly ISerializer Serializer;

        static PropertyDictionaryExtension()
        {
            Serializer = new SerializerFactory().Create();
        }
        
        public static PropertyDictionary ToLocal(this WireProtocol.PropertyDictionary propertyDictionary)
        {
            using (var textReader = new StringReader(propertyDictionary.Data))
            {
                using (var jsonReader = new JsonTextReader(textReader))
                {
                    return Serializer.Deserialize<PropertyDictionary>(jsonReader);
                }
            }
        }

        public static WireProtocol.PropertyDictionary ToWire(this PropertyDictionary propertyDictionary)
        {
            var stringBuilder = new StringBuilder();
            using (var textWriter = new StringWriter(stringBuilder))
            {
                Serializer.Serialize(textWriter, propertyDictionary);
            }        
            
            return new WireProtocol.PropertyDictionary
            {
                Data = stringBuilder.ToString()
            };
        }

        public static IEnumerable<WireProtocol.PropertyDictionary> ToWire(this IEnumerable<PropertyDictionary> propertyDictionarys)
        {
            return propertyDictionarys.Select(s => s.ToWire());
        }
    }
}
