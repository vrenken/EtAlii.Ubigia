// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Grpc;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport.Grpc;
using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
using global::Grpc.Core;
using Storage = EtAlii.Ubigia.Storage;

public partial class GrpcInformationDataClient
{
    public async Task<Storage> GetConnectedStorage(IStorageConnection connection)
    {
        var grpcConnection = (IGrpcStorageConnection)connection;
        SetClients(grpcConnection.Transport.CallInvoker);

        if (connection.Storage != null)
        {
            throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
        }

        var storage = await GetConnectedStorage(grpcConnection.Transport).ConfigureAwait(false);
        if (storage == null)
        {
            throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToStorage);
        }

        return storage;
    }

    private async Task<Storage> GetConnectedStorage(IGrpcTransport transport)
    {
        var metadata = new Metadata { transport.AuthenticationHeader };
        var request = new StorageSingleRequest();
        var response = await _storageClient.GetLocalAsync(request, metadata);
        var storage = response.Storage.ToLocal();
        return storage;
    }

    public async Task<EtAlii.Ubigia.Api.Transport.ConnectivityDetails> GetConnectivityDetails(IStorageConnection connection)
    {
        var grpcConnection = (IGrpcStorageConnection)connection;
        SetClients(grpcConnection.Transport.CallInvoker);

        var metadata = new Metadata { grpcConnection.Transport.AuthenticationHeader };
        var request = new ConnectivityDetailsRequest();
        var response = await _client.GetLocalConnectivityDetailsAsync(request, metadata);
        var connectivityDetails = response.ConnectivityDetails.ToLocal();
        return connectivityDetails;
    }
}
