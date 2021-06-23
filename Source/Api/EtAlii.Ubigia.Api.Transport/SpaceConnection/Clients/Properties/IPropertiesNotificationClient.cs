// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

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
