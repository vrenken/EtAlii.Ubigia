namespace EtAlii.Ubigia.Serialization
{
    using System.IO;

    public interface IPropertiesSerializer
    {
        void Serialize(Stream stream, PropertyDictionary item);
        PropertyDictionary Deserialize(Stream stream);
    }
}
