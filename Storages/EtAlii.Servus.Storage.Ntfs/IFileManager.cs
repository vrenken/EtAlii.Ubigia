namespace EtAlii.Servus.Storage
{
    public interface IFileManager
    {
        void SaveToFile<T>(string path, T item)
            where T : class;

        T LoadFromFile<T>(string path)
            where T : class;
    }
}
