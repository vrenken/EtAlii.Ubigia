// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using EtAlii.Ubigia.Api.Transport;
using EtAlii.Ubigia.Api.Transport.Management;

internal class SystemStorageConnection : StorageConnection<SystemStorageTransport>, ISystemStorageConnection
{
    public SystemStorageConnection(
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
