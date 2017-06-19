namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.IO;
    using Newtonsoft.Json;

    // TODO: Should be moved to EtAlii.Ubigia.Api.Transport assembly.
    public interface ISerializer
    {
        DefaultValueHandling DefaultValueHandling { get; }
        JsonConverterCollection Converters { get; }
        Formatting Formatting { get; set; }

        object Deserialize(JsonReader reader);
        object Deserialize(TextReader reader, Type objectType);
        T Deserialize<T>(JsonReader reader);
        object Deserialize(JsonReader reader, Type objectType);
        void Serialize(TextWriter textWriter, object value);
        void Serialize(JsonWriter jsonWriter, object value, Type objectType);
        void Serialize(TextWriter textWriter, object value, Type objectType);
        void Serialize(JsonWriter jsonWriter, object value);
    }
}