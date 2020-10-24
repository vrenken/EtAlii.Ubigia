namespace EtAlii.Ubigia.Persistence
{
    using System.IO;

    public interface IInternalItemSerializer
    {
        string FileNameFormat { get; }

        void Serialize<T>(Stream stream, T item)
            where T : class;
        T Deserialize<T>(Stream stream)
            where T : class;
    }
}
