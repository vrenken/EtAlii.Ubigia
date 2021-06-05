namespace EtAlii.Ubigia.Api.Transport.Management.Rest
{
    using EtAlii.Ubigia.Api.Transport.Rest;

    internal class RestStorageConnection : StorageConnection<RestStorageTransport>, IRestStorageConnection
    {
        public IInfrastructureClient Client { get; }

        public IAddressFactory AddressFactory { get; }

        public RestStorageConnection(
            IStorageTransport transport,
            IAddressFactory addressFactory,
            IInfrastructureClient client,
            IStorageConnectionConfiguration configuration,
            IStorageContext storages,
            ISpaceContext spaces,
            IAccountContext accounts,
            IAuthenticationManagementContext authentication,
            IInformationContext information)
            : base(transport, configuration, storages, spaces, accounts, authentication, information)
        {
            AddressFactory = addressFactory;
            Client = client;
        }
    }
}
