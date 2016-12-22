namespace EtAlii.Servus.Api.Transport.SignalR
{
    using System.Threading.Tasks;
    using System.Net;
    using Microsoft.AspNet.SignalR.Client.Http;

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
                signalRConnection.Transport.HttpClient,
                signalRConnection.Configuration.Address,
                signalRConnection.Configuration.AccountName,
                signalRConnection.Configuration.Password,
                signalRConnection.Transport.AuthenticationToken);

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
                signalRConnection.Transport.HttpClient,
                signalRConnection.Configuration.Address,
                signalRConnection.Configuration.AccountName,
                signalRConnection.Configuration.Password,
                signalRConnection.Transport.AuthenticationToken);

            if (storage == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToStorage);
            }

            //// We do not want the address pushed to us from the server. 
            //// If we get here then we already know how to contact the server. 
            storage.Address = connection.Configuration.Address;

            return storage;
        }

        private async Task<Storage> GetConnectedStorage(IHttpClient httpClient, string address, string accountName, string password, string authenticationToken)
        {
            var hubConnection = new HubConnectionFactory().Create(address + RelativeUri.UserData);
            hubConnection.Headers["Authentication-Token"] = authenticationToken;
            hubConnection.Credentials = new NetworkCredential(accountName, password);
            var proxy = hubConnection.CreateHubProxy(SignalRHub.Authentication);
            await hubConnection.Start(httpClient);
            var storage = await _invoker.Invoke<Storage>(proxy, SignalRHub.Authentication, "GetLocalStorage");
            hubConnection.Stop();
            return storage;
        }
    }
}
