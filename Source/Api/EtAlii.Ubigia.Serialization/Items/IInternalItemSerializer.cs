namespace EtAlii.Ubigia.Serialization
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IInternalItemSerializer
    {
        string FileNameFormat { get; }

        void Serialize<T>(Stream stream, T item)
            where T : class;
        Task<T> Deserialize<T>(Stream stream)
            where T : class;
    }
}
