// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest;

internal class RestSpaceConnection : SpaceConnection<RestSpaceTransport>, IRestSpaceConnection
{
    /// <inheritdoc />
    public IRestInfrastructureClient Client { get; }

    /// <inheritdoc />
    public IAddressFactory AddressFactory { get; }

    public RestSpaceConnection(
        ISpaceTransport transport,
        IAddressFactory addressFactory,
        IRestInfrastructureClient client,
        SpaceConnectionOptions options,
        IRootContext roots,
        IEntryContext entries,
        IContentContext content,
        IPropertiesContext properties,
        IAuthenticationContext authentication)
        : base(transport, options, roots, entries, content, properties, authentication)
    {
        AddressFactory = addressFactory;
        Client = client;
    }
}
