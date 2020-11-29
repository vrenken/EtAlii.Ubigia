namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
    using Storage = EtAlii.Ubigia.Storage;

    public partial class GrpcInformationDataClient
    {
        public async Task<Storage> GetConnectedStorage(IStorageConnection connection)
        {
            var grpcConnection = (IGrpcStorageConnection)connection;
            SetClients(grpcConnection.Transport.Channel);

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
            var request = new StorageSingleRequest();
            var response = await _storageClient.GetLocalAsync(request, transport.AuthenticationHeaders);
            var storage = response.Storage.ToLocal();
            return storage;
        }

        public async Task<EtAlii.Ubigia.Api.Transport.ConnectivityDetails> GetConnectivityDetails(IStorageConnection storageConnection)
        {
            var grpcConnection = (IGrpcStorageConnection)storageConnection;
            SetClients(grpcConnection.Transport.Channel);

            var request = new ConnectivityDetailsRequest();
            var response = await _client.GetLocalConnectivityDetailsAsync(request, grpcConnection.Transport.AuthenticationHeaders);
            var connectivityDetails = response.ConnectivityDetails.ToLocal();
            return connectivityDetails;
        }
    }
}
