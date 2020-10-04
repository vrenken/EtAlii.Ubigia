namespace EtAlii.Ubigia.Persistence
{
    using System.IO;

    public interface IInternalPropertiesSerializer
    {
        string FileNameFormat { get; }

        void Serialize(Stream stream, PropertyDictionary item);
        PropertyDictionary Deserialize(Stream stream);
    }
}
