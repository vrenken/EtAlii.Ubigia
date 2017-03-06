namespace EtAlii.Ubigia.Api.Transport.Management.WebApi
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    internal class WebApiStorageConnection : StorageConnection<WebApiStorageTransport>, IWebApiStorageConnection
    {
        public IInfrastructureClient Client => _client;
        private readonly IInfrastructureClient _client;

        public IAddressFactory AddressFactory => _addressFactory;
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