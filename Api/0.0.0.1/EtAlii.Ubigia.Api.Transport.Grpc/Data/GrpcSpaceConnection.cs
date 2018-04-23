namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    internal class GrpcSpaceConnection : SpaceConnection<GrpcSpaceTransport>, IGrpcSpaceConnection
    {
        public GrpcSpaceConnection(
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