// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    public interface IStorageNotificationClient : IStorageTransportClient
    {
    }

    public interface IStorageNotificationClient<in TTransport> : IStorageNotificationClient, IStorageTransportClient<TTransport>
        where TTransport : IStorageTransport
    {
    }
}
