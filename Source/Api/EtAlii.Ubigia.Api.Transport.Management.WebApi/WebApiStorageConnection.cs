namespace EtAlii.Ubigia.Api.Transport.Management.WebApi
{
    using EtAlii.Ubigia.Api.Transport.WebApi;

    internal class WebApiStorageConnection : StorageConnection<WebApiStorageTransport>, IWebApiStorageConnection
    {
        public IInfrastructureClient Client { get; }

        public IAddressFactory AddressFactory { get; }

        public WebApiStorageConnection(
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