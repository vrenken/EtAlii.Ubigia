// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    internal class GrpcSpaceConnection : SpaceConnection<IGrpcSpaceTransport>, IGrpcSpaceConnection
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
