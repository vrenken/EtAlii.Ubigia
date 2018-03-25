namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System.Threading.Tasks;

    public partial class SignalRAuthenticationDataClient : SignalRClientBase, IAuthenticationDataClient<ISignalRSpaceTransport>
    {
        public async Task<Storage> GetConnectedStorage(ISpaceConnection connection)
        {
            if (connection.Storage != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var signalRConnection = (ISignalRSpaceConnection) connection;
            var storage = await GetConnectedStorage(
	            signalRConnection.Transport,
				signalRConnection.Configuration.Address);

            if (storage == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToStorage);
            }

            //// We do not want the address pushed to us from the server. 
            //// If we get here then we already know how to contact the server. 
            storage.Address = connection.Configuration.Address;

            return storage;
        }

        public async Task<Storage> GetConnectedStorage(IStorageConnection connection)
        {
            if (connection.Storage != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var signalRConnection = (ISignalRStorageConnection)connection;

            var storage = await GetConnectedStorage(
	            signalRConnection.Transport,
				signalRConnection.Configuration.Address);

            if (storage == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToStorage);
            }

            //// We do not want the address pushed to us from the server. 
            //// If we get here then we already know how to contact the server. 
            storage.Address = connection.Configuration.Address;

            return storage;
        }

        private async Task<Storage> GetConnectedStorage(
	        ISignalRTransport transport,
	        string address)
        {
			   var connection = new HubConnectionFactory().Create(transport.HttpMessageHandler, address + SignalRHub.BasePath + "/" + SignalRHub.Authentication, transport.AuthenticationToken);
            await connection.StartAsync();
            var storage = await _invoker.Invoke<Storage>(connection, SignalRHub.Authentication, "GetLocalStorage");
            await connection.DisposeAsync();
            return storage;
        }
    }
}
