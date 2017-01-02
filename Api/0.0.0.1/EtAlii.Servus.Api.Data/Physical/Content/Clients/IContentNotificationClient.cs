namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System;

    public interface IContentNotificationClient : INotificationClient
    {
        event Action<Identifier> Updated;
        event Action<Identifier> Stored;
    }
}
