namespace EtAlii.Ubigia.Persistence
{
    using System.IO;
    using Newtonsoft.Json;

    // TODO: Should be moved to EtAlii.Ubigia.Api.Transport assembly.
    // Update 2017-06-23: This should really be done. storage and transportation should be two
    // distinct and separated serializations (e.g. prot buff for storage).
    // Update 2019-08-16: Don't know yet really. It should not be centralized as indeed
    // on-medium protobuf storage would be really cool. The Transport project has an implementation already so we can
    // clean this instance without problems. It could make future protobuf storage easier.
    // Tty in a few years time :-)
    public interface ISerializer
    {
        Formatting Formatting { get; set; }

        T Deserialize<T>(JsonReader reader);
        void Serialize(TextWriter textWriter, object value);
        void Serialize(JsonWriter jsonWriter, object value);
    }
}