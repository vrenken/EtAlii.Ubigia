namespace EtAlii.Ubigia.Storage
{
    using EtAlii.Ubigia.Api;

    public interface IStorageSerializer
    {
        string FileNameFormat { get; }

        void Serialize<T>(string fileName, T item)
            where T : class;

        void Serialize(string fileName, PropertyDictionary item);

        T Deserialize<T>(string fileName)
            where T : class;

        PropertyDictionary Deserialize(string fileName);
    }
}
