namespace EtAlii.Ubigia.Api.Transport
{
    public interface ISpaceNotificationClient : IStorageTransportClient
    {
    }

    public interface ISpaceNotificationClient<in TTransport> : ISpaceNotificationClient, IStorageTransportClient<TTransport>
        where TTransport : IStorageTransport
    {
    }
}
