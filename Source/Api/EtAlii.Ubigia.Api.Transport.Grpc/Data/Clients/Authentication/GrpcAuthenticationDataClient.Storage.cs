// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
using Storage = EtAlii.Ubigia.Storage;
using global::Grpc.Core;

public partial class GrpcAuthenticationDataClient
{
    public async Task<Storage> GetConnectedStorage(ISpaceConnection connection)
    {
        if (connection.Storage != null)
        {
            throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
        }

        var grpcConnection = (IGrpcSpaceConnection) connection;
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
        var response = await _storageClient.GetLocalAsync(request, metadata).ConfigureAwait(false);
        var storage = response.Storage.ToLocal();
        return storage;
    }
}
