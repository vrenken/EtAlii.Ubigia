namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    internal class WebApiSpaceConnection : SpaceConnection<WebApiSpaceTransport>, IWebApiSpaceConnection
    {
        public IInfrastructureClient Client { get; }

        public IAddressFactory AddressFactory { get; }

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
            AddressFactory = addressFactory;
            Client = client;
        }
    }
}