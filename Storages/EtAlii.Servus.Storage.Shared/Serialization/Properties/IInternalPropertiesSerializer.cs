namespace EtAlii.Servus.Storage
{
    using System.IO;
    using EtAlii.Servus.Api;

    public interface IInternalPropertiesSerializer
    {
        string FileNameFormat { get; }

        void Serialize(Stream stream, PropertyDictionary item);
        PropertyDictionary Deserialize(Stream stream);
    }
}
