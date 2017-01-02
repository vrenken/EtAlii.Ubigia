namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System.Threading.Tasks;

    internal class WebApiSpaceConnection : SpaceConnection<WebApiSpaceTransport>, IWebApiSpaceConnection
    {
        public IInfrastructureClient Client { get { return _client; } }
        private readonly IInfrastructureClient _client;

        public IAddressFactory AddressFactory { get { return _addressFactory; } }
        private readonly IAddressFactory _addressFactory;

        public WebApiSpaceConnection(
            ISpaceTransport transport, 
            IAddressFactory addressFactory, 
            IInfrastructureClient client, 
            ISpaceConnectionConfiguration configuration, 
            IRootContext roots, 
            IEntryContext entries, 
            IContentContext content, 
            IPropertyContext properties, 
            IAuthenticationContext authentication) 
            : base(transport, configuration, roots, entries, content, properties, authentication)
        {
            _addressFactory = addressFactory;
            _client = client;
        }
    }
}