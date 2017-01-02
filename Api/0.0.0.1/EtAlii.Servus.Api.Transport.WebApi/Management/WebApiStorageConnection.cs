namespace EtAlii.Servus.Api.Management.WebApi
{
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.WebApi;

    internal class WebApiStorageConnection : StorageConnection<WebApiStorageTransport>, IWebApiStorageConnection
    {
        public IInfrastructureClient Client { get { return _client; } }
        private readonly IInfrastructureClient _client;

        public IAddressFactory AddressFactory { get { return _addressFactory; } }
        private readonly IAddressFactory _addressFactory;

        public WebApiStorageConnection(
            IStorageTransport transport, 
            IAddressFactory addressFactory, 
            IInfrastructureClient client, 
            IStorageConnectionConfiguration configuration, 
            IStorageContext storages, 
            ISpaceContext spaces, 
            IAccountContext accounts,
            IAuthenticationContext authentication) 
            : base(transport, configuration, storages, spaces, accounts, authentication)
        {
            _addressFactory = addressFactory;
            _client = client;
        }
    }
}