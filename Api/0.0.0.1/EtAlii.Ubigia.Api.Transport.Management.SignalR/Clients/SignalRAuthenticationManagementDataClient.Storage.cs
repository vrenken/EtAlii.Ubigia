namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.SignalR;

    public partial class SignalRAuthenticationManagementDataClient
    {
        public async Task<Storage> GetConnectedStorage(IStorageConnection storageConnection)
        {
            if (storageConnection.Storage != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.StorageAlreadyOpen);
            }

            var signalRConnection = (ISignalRStorageConnection)storageConnection;

            var storage = await GetConnectedStorage(signalRConnection.Transport);

            if (storage == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToStorage);
            }

            //// We do not want the address pushed to us from the server. 
            //// If we get here then we already know how to contact the server. 
            storage.Address = storageConnection.Transport.Address.ToString();

            return storage;
        }

        private async Task<Storage> GetConnectedStorage(ISignalRStorageTransport transport)
        {
			var connection = new HubConnectionFactory().Create(transport,new Uri(transport.Address + SignalRHub.BasePath + SignalRHub.Authentication), transport.AuthenticationToken);
            await connection.StartAsync();
            var storage = await _invoker.Invoke<Storage>(connection, SignalRHub.Authentication, "GetLocalStorage");
            await connection.DisposeAsync();
            return storage;
        }
    }
}
