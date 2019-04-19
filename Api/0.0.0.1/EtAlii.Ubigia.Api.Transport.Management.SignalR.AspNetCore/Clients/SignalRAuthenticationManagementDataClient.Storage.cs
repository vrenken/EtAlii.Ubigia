namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.SignalR;

    public partial class SignalRAuthenticationManagementDataClient
    {
        public async Task<Storage> GetConnectedStorage(IStorageConnection connection)
        {
            if (connection.Storage != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.StorageAlreadyOpen);
            }

            var signalRConnection = (ISignalRStorageConnection)connection;

            var storage = await GetConnectedStorage(signalRConnection.Transport);

            if (storage == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToStorage);
            }

            //// We do not want the address pushed to us from the server. 
            //// If we get here then we already know how to contact the server. 
            storage.Address = connection.Transport.Address.ToString();

            return storage;
        }

        private async Task<Storage> GetConnectedStorage(ISignalRTransport transport)
        {
			var connection = new HubConnectionFactory().Create(transport,new Uri(transport.Address + SignalRHub.BasePath + SignalRHub.Authentication), transport.AuthenticationToken);
            await connection.StartAsync();
            var storage = await _invoker.Invoke<Storage>(connection, SignalRHub.Authentication, "GetLocalStorage");
            await connection.DisposeAsync();
            return storage;
        }
    }
}
