namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public interface IStorageTransportClient
    {
        Task Connect(IStorageConnection connection);
        Task Disconnect(IStorageConnection connection);
    }

    public interface IStorageTransportClient<in TTransport> : IStorageTransportClient
        where TTransport : IStorageTransport
    {
        Task Connect(IStorageConnection<TTransport> connection);
        Task Disconnect(IStorageConnection<TTransport> connection);
    }
}
