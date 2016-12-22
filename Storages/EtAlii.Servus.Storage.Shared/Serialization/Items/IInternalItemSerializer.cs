namespace EtAlii.Servus.Storage
{
    using System.IO;
    using EtAlii.Servus.Api;

    public interface IInternalItemSerializer
    {
        string FileNameFormat { get; }

        void Serialize<T>(Stream stream, T item)
            where T : class;
        T Deserialize<T>(Stream stream)
            where T : class;
    }
}
