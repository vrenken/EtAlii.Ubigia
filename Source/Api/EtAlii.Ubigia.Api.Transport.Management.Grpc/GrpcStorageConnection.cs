// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Grpc;

using EtAlii.Ubigia.Api.Transport.Grpc;

internal class GrpcStorageConnection : StorageConnection<IGrpcStorageTransport>, IGrpcStorageConnection
{
    public GrpcStorageConnection(
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
