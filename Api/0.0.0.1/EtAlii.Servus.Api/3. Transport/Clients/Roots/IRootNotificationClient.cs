namespace EtAlii.Servus.Api.Transport
{
    using System;

    public interface IRootNotificationClient : ISpaceTransportClient
    {
        event Action<Guid> Added;
        event Action<Guid> Changed;
        event Action<Guid> Removed;
    }

    public interface IRootNotificationClient<in Ttransport> : IRootNotificationClient, ISpaceTransportClient<Ttransport>
        where Ttransport: ISpaceTransport
    {
    }
}
