namespace EtAlii.Servus.Infrastructure.Transport
{
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Management;

    internal class SystemStorageConnection : StorageConnection<SystemStorageTransport>, ISystemStorageConnection
    {
        public SystemStorageConnection(
            IStorageTransport transport,
            IStorageConnectionConfiguration configuration,
            IStorageContext storages,
            ISpaceContext spaces,
            IAccountContext accounts, 
            IAuthenticationContext authentication)
            : base(transport, configuration, storages, spaces, accounts, authentication)
        {
        }
        
    }
}
