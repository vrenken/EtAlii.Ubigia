// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    public interface IAccountNotificationClient : IStorageTransportClient
    {
    }

    public interface IAccountNotificationClient<in TTransport> : IAccountNotificationClient, IStorageTransportClient<TTransport>
        where TTransport: IStorageTransport
    {
    }
}
