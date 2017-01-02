namespace EtAlii.Servus.Api.Logical
{
    using System.IO;

    public interface IPropertiesSerializer
    {
        void Serialize(PropertyDictionary properties, Stream stream);
        PropertyDictionary Deserialize(Stream stream);
    }
}
