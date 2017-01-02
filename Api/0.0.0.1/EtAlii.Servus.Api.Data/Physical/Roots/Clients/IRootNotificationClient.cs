namespace EtAlii.Servus.Api.Data
{
    using System;

    public interface IRootNotificationClient : INotificationClient
    {
        event Action<Guid> Added;
        event Action<Guid> Changed;
        event Action<Guid> Removed;
    }
}
