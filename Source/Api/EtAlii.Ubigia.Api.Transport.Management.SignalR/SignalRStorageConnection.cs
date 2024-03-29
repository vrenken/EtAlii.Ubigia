// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.SignalR;

using EtAlii.Ubigia.Api.Transport.SignalR;

internal class SignalRStorageConnection : StorageConnection<ISignalRStorageTransport>, ISignalRStorageConnection
{
    public SignalRStorageConnection(
        IStorageTransport transport,
        IStorageConnectionOptions options,
        IStorageContext storages,
        ISpaceContext spaces,
        IAccountContext accounts,
        IAuthenticationManagementContext authentication,
        IInformationContext information)
        : base(transport, options, storages, spaces, accounts, authentication, information)
    {
    }
}
