namespace EtAlii.Servus.Infrastructure.Transport
{
    using EtAlii.Servus.Api.Transport;

    internal class SystemSpaceConnection : SpaceConnection<SystemSpaceTransport>, ISystemSpaceConnection
    {
        public SystemSpaceConnection(
            ISpaceTransport transport, 
            ISpaceConnectionConfiguration configuration,
            IRootContext roots, 
            IEntryContext entries, 
            IContentContext content, 
            IPropertyContext properties, 
            IAuthenticationContext authentication
            ) 
            : base(transport, configuration, roots, entries, content, properties, authentication)
        {
        }
    }
}
