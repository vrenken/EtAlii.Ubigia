namespace EtAlii.Servus.Api.Transport.WebApi
{
    internal class WebApiSpaceConnection : SpaceConnection<WebApiSpaceTransport>
    {
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
            : base(transport, addressFactory, client, configuration, roots, entries, content, properties, authentication)
        {
        }
    }
}