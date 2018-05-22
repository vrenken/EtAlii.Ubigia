namespace EtAlii.Ubigia.Api.Transport.Grpc
{
	using System;
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
            var storage = await GetConnectedStorage(
                grpcConnection.Transport,
                grpcConnection.Configuration.Address);

            if (storage == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToStorage);
            }

            //// We do not want the address pushed to us from the server. 
            //// If we get here then we already know how to contact the server. 
            storage.Address = connection.Configuration.Address.ToString();

            return storage;
        }

        public async Task<Api.Storage> GetConnectedStorage(IStorageConnection connection)
        {
            if (connection.Storage != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var grpcConnection = (IGrpcStorageConnection)connection;

            var storage = await GetConnectedStorage(
                grpcConnection.Transport,
                grpcConnection.Configuration.Address);

            if (storage == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToStorage);
            }

            //// We do not want the address pushed to us from the server. 
            //// If we get here then we already know how to contact the server. 
            storage.Address = connection.Configuration.Address.ToString();

            return storage;
        }

        private async Task<Api.Storage> GetConnectedStorage(
	        IGrpcTransport transport,
	        Uri address)
        {
            var request = new StorageSingleRequest{ };
            var response = await _storageClient.GetLocalAsync(request, transport.AuthenticationHeaders);
            var storage = response.Storage.ToLocal();
            //var storage = await _invoker.Invoke<Storage>(connection, GrpcHub.Authentication, "GetLocalStorage");
            return storage;
        }
    }
}
