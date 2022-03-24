// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization
{
	using System.Collections.Generic;
	using Newtonsoft.Json;

	public sealed class SerializerFactory
    {
        public ISerializer Create()
        {
            var serializer = new Serializer
            {
                ContractResolver = new FieldBasedContractResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                TypeNameHandling = TypeNameHandling.None,
            };

            AddDefaultConverters(serializer.Converters);

            return serializer;
        }

	    public static void Configure(JsonSerializerSettings settings)
	    {
		    settings.ContractResolver = new FieldBasedContractResolver();
		    settings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
		    settings.DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate;
		    settings.TypeNameHandling = TypeNameHandling.None;
		    AddDefaultConverters(settings.Converters);
	    }

	    public static JsonSerializerSettings CreateSerializerSettings()
	    {
		    var settings = new JsonSerializerSettings();
			Configure(settings);
		    return settings;
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
