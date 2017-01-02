namespace EtAlii.Ubigia.Api.Management.SignalR
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.SignalR;

    internal class SignalRStorageConnection : StorageConnection<ISignalRStorageTransport>, ISignalRStorageConnection
    {
        public SignalRStorageConnection(
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