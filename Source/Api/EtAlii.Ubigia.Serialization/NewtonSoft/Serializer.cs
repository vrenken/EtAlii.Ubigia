// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public sealed class Serializer : JsonSerializer, ISerializer
    {
        public static readonly ISerializer Default;

        public static readonly JsonSerializerSettings DefaultSettings;

        static Serializer()
        {
            var serializer = new Serializer
            {
                ContractResolver = new FieldBasedContractResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                TypeNameHandling = TypeNameHandling.None,
            };

            AddDefaultConverters(serializer.Converters);

            Default = serializer;

            DefaultSettings = new JsonSerializerSettings();
            Configure(DefaultSettings);
        }

        public static void Configure(JsonSerializerSettings settings)
        {
            settings.ContractResolver = new FieldBasedContractResolver();
            settings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
            settings.DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate;
            settings.TypeNameHandling = TypeNameHandling.None;
            AddDefaultConverters(settings.Converters);
        }

        private static void AddDefaultConverters(ICollection<JsonConverter> converters)
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
