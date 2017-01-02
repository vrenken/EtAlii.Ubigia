namespace EtAlii.Ubigia.Storage
{
    using EtAlii.Ubigia.Api;

    public interface IImmutableFileManager
    {
        void SaveToFile<T>(string path, T item)
            where T : class;

        T LoadFromFile<T>(string path)
            where T : class;

        void SaveToFile(string path, PropertyDictionary item);
        PropertyDictionary LoadFromFile(string path);

        bool Exists(string path);
    }
}
