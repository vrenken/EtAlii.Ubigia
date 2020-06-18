namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;

    public partial class GrpcInformationDataClient
    {
        public async Task<Api.Storage> GetConnectedStorage(IStorageConnection storageConnection)
        {
            var grpcConnection = (IGrpcStorageConnection)storageConnection;
            SetClients(grpcConnection.Transport.Channel);

            if (storageConnection.Storage != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var storage = await GetConnectedStorage(grpcConnection.Transport);
            if (storage == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToStorage);
            }

            //// We do not want the address pushed to us from the server. 
            //// If we get here then we already know how to contact the server. 
            storage.Address = storageConnection.Transport.Address.ToString();

            return storage;
        }

        private async Task<Api.Storage> GetConnectedStorage(IGrpcTransport transport)
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
