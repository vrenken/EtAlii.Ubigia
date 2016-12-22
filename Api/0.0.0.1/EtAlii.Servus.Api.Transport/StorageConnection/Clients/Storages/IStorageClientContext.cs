namespace EtAlii.Servus.Api.Transport
{
    using System.Threading.Tasks;

    public interface IStorageClientContext
    {
        Task Open(IStorageConnection storageConnection);
        Task Close(IStorageConnection storageConnection);
    }
}