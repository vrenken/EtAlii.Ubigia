namespace EtAlii.Servus.Api.Transport.SignalR
{
    internal class SignalRSpaceConnection : SpaceConnection<SignalRSpaceTransport>
    {
        public SignalRSpaceConnection(
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