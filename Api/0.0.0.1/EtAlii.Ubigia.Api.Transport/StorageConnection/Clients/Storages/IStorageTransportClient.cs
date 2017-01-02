namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public interface IStorageTransportClient
    {
        Task Connect(IStorageConnection storageConnection);
        Task Disconnect(IStorageConnection storageConnection);
    }

    public interface IStorageTransportClient<in TTransport> : IStorageTransportClient
        where TTransport : IStorageTransport
    {
        Task Connect(IStorageConnection<TTransport> storageConnection);
        Task Disconnect(IStorageConnection<TTransport> storageConnection);
    }
}
