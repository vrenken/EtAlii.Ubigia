// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest
{
    internal class RestSpaceConnection : SpaceConnection<RestSpaceTransport>, IRestSpaceConnection
    {
        public IRestInfrastructureClient Client { get; }

        public IAddressFactory AddressFactory { get; }

        public RestSpaceConnection(
            ISpaceTransport transport,
            IAddressFactory addressFactory,
            IRestInfrastructureClient client,
            ISpaceConnectionConfiguration configuration,
            IRootContext roots,
            IEntryContext entries,
            IContentContext content,
            IPropertiesContext properties,
            IAuthenticationContext authentication)
            : base(transport, configuration, roots, entries, content, properties, authentication)
        {
            AddressFactory = addressFactory;
            Client = client;
        }
    }
}
