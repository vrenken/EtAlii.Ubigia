namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Grpc;

    internal class GrpcStorageConnection : StorageConnection<IGrpcStorageTransport>, IGrpcStorageConnection
    {
        public GrpcStorageConnection(
            IStorageTransport transport, 
            IStorageConnectionConfiguration configuration, 
            IStorageContext storages, 
            ISpaceContext spaces, 
            IAccountContext accounts,
            IAuthenticationManagementContext authentication) 
            : base(transport, configuration, storages, spaces, accounts, authentication)
        {
        }
    }
}