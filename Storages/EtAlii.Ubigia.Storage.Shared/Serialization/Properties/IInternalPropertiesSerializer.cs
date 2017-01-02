namespace EtAlii.Ubigia.Storage
{
    using System.IO;
    using EtAlii.Ubigia.Api;

    public interface IInternalPropertiesSerializer
    {
        string FileNameFormat { get; }

        void Serialize(Stream stream, PropertyDictionary item);
        PropertyDictionary Deserialize(Stream stream);
    }
}
