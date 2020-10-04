namespace EtAlii.Ubigia.Persistence
{
    public interface IImmutableFileManager
    {
        void SaveToFile<T>(string path, T item)
            where T : class;
        void SaveToFile(string path, PropertyDictionary item);

        T LoadFromFile<T>(string path)
            where T : class;

        PropertyDictionary LoadFromFile(string path);

        bool Exists(string path);
    }
}
