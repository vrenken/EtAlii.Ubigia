﻿namespace EtAlii.Ubigia.Persistence
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class SerializerFactory
    {
        public ISerializer Create()
        {
            var serializer = new Serializer
            {
                ContractResolver = new FieldBasedContractResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
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
            //// We want to serialize all the Ubigia specific classes.
            //converters.Add[new EntryJsonConverter[]]

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
