// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Rest;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport.Rest;

internal sealed class RestInformationDataClient : RestClientBase, IInformationDataClient
{
    public async Task<Storage> GetConnectedStorage(IStorageConnection connection)
    {
        if (connection.Storage != null)
        {
            throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.StorageAlreadyOpen);
        }

        var restConnection = (IRestConnection)connection;
        var localAddress = restConnection.AddressFactory.Create(connection.Transport.Address, RelativeDataUri.Storages, UriParameter.Local);
        var storage = await restConnection.Client.Get<Storage>(localAddress).ConfigureAwait(false);

        if (storage == null)
        {
            throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToStorage);
        }

        return storage;
    }

    public async Task<ConnectivityDetails> GetConnectivityDetails(IStorageConnection connection)
    {
        var restConnection = (IRestConnection)connection;
        var address = restConnection.AddressFactory.Create(connection.Transport.Address, RelativeDataUri.Information, UriParameter.Connectivity);
        var connectivityDetails = await restConnection.Client.Get<ConnectivityDetails>(address).ConfigureAwait(false);
        return connectivityDetails;
    }
}
