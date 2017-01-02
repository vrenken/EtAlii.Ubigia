namespace EtAlii.Servus.Storage
{

    public interface IItemSerializer
    {
        string FileNameFormat { get; }

        void Serialize<T>(string fileName, T item)
            where T : class;
        
        T Deserialize<T>(string fileName)
            where T : class;
    }
}
