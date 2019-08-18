namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    internal class SignalRSpaceConnection : SpaceConnection<SignalRSpaceTransport>, ISignalRSpaceConnection
    {
        public SignalRSpaceConnection(
            ISpaceTransport transport, 
            ISpaceConnectionConfiguration configuration, 
            IRootContext roots, 
            IEntryContext entries, 
            IContentContext content, 
            IPropertiesContext properties, 
            IAuthenticationContext authentication) 
            : base(transport, configuration, roots, entries, content, properties, authentication)
        {
        }
    }
}