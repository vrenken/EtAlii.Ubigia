// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace Docker.DotNet.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Facade for <see cref="JsonConvert"/>.
    /// </summary>
    internal class JsonSerializer
    {
        private readonly JsonSerializerSettings _settings = new()
        {
            NullValueHandling = NullValueHandling.Ignore,
            Converters = new JsonConverter[]
            {
                //new JsonIso8601AndUnixEpochDateConverter(),
                //new JsonVersionConverter(),
                new StringEnumConverter(),
                //new TimeSpanSecondsConverter(),
                //new TimeSpanNanosecondsConverter()
            }
        };

        public T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, _settings);
        }

        public string SerializeObject<T>(T value)
        {
            return JsonConvert.SerializeObject(value, _settings);
        }
    }
}
