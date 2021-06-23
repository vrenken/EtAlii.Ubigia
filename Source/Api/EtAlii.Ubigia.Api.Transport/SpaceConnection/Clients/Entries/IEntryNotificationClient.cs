// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public interface IEntryNotificationClient : ISpaceTransportClient
    {
        event Action<Identifier> Prepared;
        event Action<Identifier> Stored;
    }

    public interface IEntryNotificationClient<in TTransport> : IEntryNotificationClient, ISpaceTransportClient<TTransport>
        where TTransport : ISpaceTransport
    {
    }
}
