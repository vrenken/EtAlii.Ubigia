// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public interface IRootNotificationClient : ISpaceTransportClient
    {
        event Action<Guid> Added;
        event Action<Guid> Changed;
        event Action<Guid> Removed;
    }

    public interface IRootNotificationClient<in TTransport> : IRootNotificationClient, ISpaceTransportClient<TTransport>
        where TTransport: ISpaceTransport
    {
    }
}
