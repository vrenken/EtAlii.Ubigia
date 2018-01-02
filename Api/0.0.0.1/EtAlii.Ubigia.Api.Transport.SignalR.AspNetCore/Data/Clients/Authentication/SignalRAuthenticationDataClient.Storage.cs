namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System.Threading.Tasks;
    using System.Net;
    using System.Net.Http;
    using Microsoft.AspNetCore.SignalR.Client.Http;

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

        private async Task<Storage> GetConnectedStorage(HttpClientHandler httpClientHandler, string address, string accountName, string password, string authenticationToken)
        {
            var connection = new HubConnectionFactory().Create(address + RelativeUri.UserData, httpClientHandler);
            httpClientHandler

            connection.Headers["Authentication-Token"] = authenticationToken;
            httpClientHandler.Credentials = new NetworkCredential(accountName, password);
            var proxy = connection.CreateHubProxy(SignalRHub.Authentication);
            await connection.StartAsync();
            var storage = await _invoker.Invoke<Storage>(proxy, SignalRHub.Authentication, "GetLocalStorage");
            await connection.DisposeAsync();
            return storage;
        }
    }
}
