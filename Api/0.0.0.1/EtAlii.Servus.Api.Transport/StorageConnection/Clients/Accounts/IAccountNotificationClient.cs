namespace EtAlii.Servus.Api.Transport
{
    public interface IAccountNotificationClient : IStorageTransportClient
    {
    }

    public interface IAccountNotificationClient<in TTransport> : IAccountNotificationClient, IStorageTransportClient<TTransport>
        where TTransport: IStorageTransport
    {
    }
}
