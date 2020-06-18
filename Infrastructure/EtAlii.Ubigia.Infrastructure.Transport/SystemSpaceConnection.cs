﻿namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.Ubigia.Api.Transport;

    internal class SystemSpaceConnection : SpaceConnection<SystemSpaceTransport>, ISystemSpaceConnection
    {
        public SystemSpaceConnection(
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
