namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System;

    public interface IEntryNotificationClient : INotificationClient
    {
        event Action<Identifier> Prepared;
        event Action<Identifier> Stored;
    }
}
