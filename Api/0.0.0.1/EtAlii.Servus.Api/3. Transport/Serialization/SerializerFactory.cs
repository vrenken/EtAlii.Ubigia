namespace EtAlii.Servus.Api.Transport
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class SerializerFactory
    {
        public ISerializer Create()
        {
            var serializer = new Serializer
            {
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                TypeNameHandling = TypeNameHandling.None,
                //DateFormatHandling = DateFormatHandling.IsoDateFormat,
                //DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
            };

            AddDefaultConverters(serializer.Converters);

            return serializer;
        }

        public static void AddDefaultConverters(ICollection<JsonConverter> converters)
        {
            // We want custom tailored unsigned long conversion. 
            // Reason for this is that we cannot trust the available ulong serialization because it is not supported by the JSON standard.
            converters.Add(new UnsignedLongJSonConverter());

            // PropertyDictionaries cannot be serialized by the standard converters, so lets use a custom one.
            converters.Add(new PropertyDictionaryJSonConverter());

            // Decimals are not completely supported by the JSON standard.
            converters.Add(new DecimalJSonConverter());

            // Default DateTime serialization loses details, kind and timezone information.
            converters.Add(new DateTimeJSonConverter());
        }
    }
}
