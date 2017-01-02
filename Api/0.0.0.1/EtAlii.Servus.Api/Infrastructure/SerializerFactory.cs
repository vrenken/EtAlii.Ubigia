namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;

    public class SerializerFactory
    {
        public JsonSerializer Create()
        {
            var serializer = new JsonSerializer
            {
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate
            };
            // We want custom tailored unsigned long conversion. 
            // Reason for this is that we cannot trust the available ulong serialization because it is not supported by the JSON standard.
            serializer.Converters.Add(new UnsignedLongJSonConverter());

            return serializer;
        }
    }
}
