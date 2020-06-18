namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;

    public partial class GrpcAuthenticationDataClient
    {
        public async Task<Api.Storage> GetConnectedStorage(ISpaceConnection connection)
        {
            if (connection.Storage != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var grpcConnection = (IGrpcSpaceConnection) connection;
            var storage = await GetConnectedStorage(grpcConnection.Transport);

            if (storage == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToStorage);
            }

            //// We do not want the address pushed to us from the server. 
            //// If we get here then we already know how to contact the server. 
            storage.Address = connection.Transport.Address.ToString();

            return storage;
        }
        private async Task<Api.Storage> GetConnectedStorage(IGrpcTransport transport)
        {
            var request = new StorageSingleRequest();
            var response = await _storageClient.GetLocalAsync(request, transport.AuthenticationHeaders);
            var storage = response.Storage.ToLocal();
            return storage;
        }
    }
}
