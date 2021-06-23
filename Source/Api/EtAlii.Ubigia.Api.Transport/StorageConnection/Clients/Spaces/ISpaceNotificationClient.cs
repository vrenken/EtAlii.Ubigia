// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    public interface ISpaceNotificationClient : IStorageTransportClient
    {
    }

    public interface ISpaceNotificationClient<in TTransport> : ISpaceNotificationClient, IStorageTransportClient<TTransport>
        where TTransport : IStorageTransport
    {
    }
}
