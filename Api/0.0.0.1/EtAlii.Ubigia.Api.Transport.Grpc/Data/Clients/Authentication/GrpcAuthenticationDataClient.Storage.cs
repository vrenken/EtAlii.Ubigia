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
            var request = new LocalStorageRequest { };
            var response = await _client.GetLocalStorageAsync(request);
            //return response.AuthenticationToken;

            // TODO: GRPC
            var storage = await Task.FromResult<Api.Storage>(null);
			//var connection = new HubConnectionFactory().Create(transport.HttpMessageHandler,new Uri(address + GrpcHub.BasePath + "/" + GrpcHub.Authentication), transport.AuthenticationToken);
            //await connection.StartAsync();
            //var storage = await _invoker.Invoke<Storage>(connection, GrpcHub.Authentication, "GetLocalStorage");
            //await connection.DisposeAsync();
            return storage;
        }
    }
}
