namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.IO;
    using Newtonsoft.Json;

    // Update 2017-06-23: This should really be done. storage and transportation should be two
    // distinct and separated serializations (e.g. prot buff for storage).
    // Update 2019-11-04: Done sometime in the past year or so.
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