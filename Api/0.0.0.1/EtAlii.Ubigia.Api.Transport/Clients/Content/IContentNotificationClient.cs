namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public interface IContentNotificationClient : ISpaceTransportClient
    {
        event Action<Identifier> Updated;
        event Action<Identifier> Stored;
    }

    public interface IContentNotificationClient<TTransport> : IContentNotificationClient, ISpaceTransportClient<TTransport>
        where TTransport: ISpaceTransport
    {
    }
}
