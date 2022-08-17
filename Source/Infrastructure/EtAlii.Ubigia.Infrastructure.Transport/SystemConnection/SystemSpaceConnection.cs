// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.Ubigia.Api.Transport;

    internal class SystemSpaceConnection : SpaceConnection<SystemSpaceTransport>, ISystemSpaceConnection
    {
        public SystemSpaceConnection(
            ISpaceTransport transport,
            SpaceConnectionOptions options,
            IRootContext roots,
            IEntryContext entries,
            IContentContext content,
            IPropertiesContext properties,
            IAuthenticationContext authentication)
            : base(transport, options, roots, entries, content, properties, authentication)
        {
        }
    }
}
