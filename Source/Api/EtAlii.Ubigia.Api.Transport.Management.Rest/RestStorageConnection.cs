// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Rest;

using EtAlii.Ubigia.Api.Transport.Rest;

internal class RestStorageConnection : StorageConnection<RestStorageTransport>, IRestStorageConnection
{
    public IRestInfrastructureClient Client { get; }

    public IAddressFactory AddressFactory { get; }

    public RestStorageConnection(
        IStorageTransport transport,
        IAddressFactory addressFactory,
        IRestInfrastructureClient client,
        IStorageConnectionOptions options,
        IStorageContext storages,
        ISpaceContext spaces,
        IAccountContext accounts,
        IAuthenticationManagementContext authentication,
        IInformationContext information)
        : base(transport, options, storages, spaces, accounts, authentication, information)
    {
        AddressFactory = addressFactory;
        Client = client;
    }
}
