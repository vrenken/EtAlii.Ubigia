namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    /// <summary>
    /// An interface that defines a  client able to work with <see cref="Content"/> specific notifications.
    /// </summary>
    public interface IContentNotificationClient : ISpaceTransportClient
    {
        event Action<Identifier> Updated;
        event Action<Identifier> Stored;
    }

    /// <summary>
    /// An interface that defines a  client able to work with <see cref="Content"/> specific notifications.
    /// </summary>
    public interface IContentNotificationClient<in TTransport> : IContentNotificationClient, ISpaceTransportClient<TTransport>
        where TTransport: ISpaceTransport
    {
    }
}
