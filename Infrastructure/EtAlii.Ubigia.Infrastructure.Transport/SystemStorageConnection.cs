namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Management;

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
