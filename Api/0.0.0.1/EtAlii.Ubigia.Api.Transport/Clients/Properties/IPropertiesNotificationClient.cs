namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public interface IPropertiesNotificationClient : ISpaceTransportClient
    {
        event Action<Identifier> Stored;
    }

    public interface IPropertiesNotificationClient<in TTransport> : IPropertiesNotificationClient, ISpaceTransportClient<TTransport>
        where TTransport: ISpaceTransport
    {
    }
}
