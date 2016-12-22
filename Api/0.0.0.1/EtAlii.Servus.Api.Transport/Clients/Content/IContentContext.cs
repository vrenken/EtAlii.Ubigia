﻿namespace EtAlii.Servus.Api.Transport
{
    public interface IContentContext : ISpaceClientContext
    {
        IContentNotificationClient Notifications { get; }
        IContentDataClient Data { get; }
    }
}