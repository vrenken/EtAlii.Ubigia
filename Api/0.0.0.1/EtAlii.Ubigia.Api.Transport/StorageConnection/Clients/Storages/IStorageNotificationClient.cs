namespace EtAlii.Ubigia.Api.Transport
{
    public interface IStorageNotificationClient : IStorageTransportClient
    {
    }

    public interface IStorageNotificationClient<TTransport> : IStorageNotificationClient, IStorageTransportClient<TTransport>
        where TTransport : IStorageTransport
    {
    }
}
