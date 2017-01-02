namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using System.IO;

    public interface IPropertiesSerializer
    {
        void Serialize(IDictionary<string, object> properties, Stream stream);
        IDictionary<string, object> Deserialize<T>(Stream stream);
    }
}
