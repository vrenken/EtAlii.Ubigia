// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using EtAlii.Ubigia.Api.Transport.Grpc;

    internal class GrpcStorageConnection : StorageConnection<IGrpcStorageTransport>, IGrpcStorageConnection
    {
        public GrpcStorageConnection(
            IStorageTransport transport, 
            IStorageConnectionConfiguration configuration, 
            IStorageContext storages, 
            ISpaceContext spaces, 
            IAccountContext accounts,
            IAuthenticationManagementContext authentication,
            IInformationContext information) 
            : base(transport, configuration, storages, spaces, accounts, authentication, information)
        {
        }
    }
}