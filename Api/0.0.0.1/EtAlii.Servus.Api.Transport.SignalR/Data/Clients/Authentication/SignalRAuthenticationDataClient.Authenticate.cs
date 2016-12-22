namespace EtAlii.Servus.Api.Transport.SignalR
{
    using System;
    using System.Threading.Tasks;
    using System.Net;
    using Microsoft.AspNet.SignalR.Client.Http;

    public partial class SignalRAuthenticationDataClient : SignalRClientBase, IAuthenticationDataClient<ISignalRSpaceTransport>
    {
        private readonly string _hostIdentifier;

        public async Task Authenticate(ISpaceConnection connection)
        {
            var signalRConnection = (ISignalRSpaceConnection)connection;
            var authenticationToken = await GetAuthenticationToken(signalRConnection.Transport.HttpClient, signalRConnection.Configuration.AccountName, signalRConnection.Configuration.Password, signalRConnection.Configuration.Address, signalRConnection.Transport.AuthenticationToken);
            
            if (!String.IsNullOrWhiteSpace(authenticationToken))
            {
                signalRConnection.Transport.AuthenticationToken = authenticationToken;
            }
            else
            {
                string message = String.Format("Unable to authenticate on the specified storage ({0})", signalRConnection.Configuration.Address);
                throw new UnauthorizedInfrastructureOperationException(message);
            }
        }

        public async Task Authenticate(IStorageConnection connection)
        {
            var signalRConnection = (ISignalRStorageConnection)connection;
            var authenticationToken = await GetAuthenticationToken(signalRConnection.Transport.HttpClient, signalRConnection.Configuration.AccountName, signalRConnection.Configuration.Password, signalRConnection.Configuration.Address, signalRConnection.Transport.AuthenticationToken);

            if (!String.IsNullOrWhiteSpace(authenticationToken))
            {
                signalRConnection.Transport.AuthenticationToken = authenticationToken;
            }
            else
            {
                string message = String.Format("Unable to authenticate on the specified storage ({0})", signalRConnection.Configuration.Address);
                throw new UnauthorizedInfrastructureOperationException(message);
            }
        }

        private async Task<string> GetAuthenticationToken(IHttpClient httpClient, string accountName, string password, string address, string authenticationToken)
        {
            if (password != null || authenticationToken == null)
            {
                var connection = new HubConnectionFactory().Create(address + RelativeUri.UserData);
                connection.Headers["Host-Identifier"] = _hostIdentifier;
                connection.Credentials = new NetworkCredential(accountName, password);
                var authenticationProxy = connection.CreateHubProxy(SignalRHub.Authentication);
                await connection.Start(httpClient);
                authenticationToken = await _invoker.Invoke<string>(authenticationProxy, SignalRHub.Authentication, "Authenticate", accountName, password, _hostIdentifier);
                connection.Stop();
            }

            if (String.IsNullOrWhiteSpace(authenticationToken))
            {
                throw new UnableToAuthorizeInfrastructureOperationException(InvalidInfrastructureOperation.UnableToAthorize);
            }
            return authenticationToken;
        }

        private string CreateHostIdentifier()
        {
            var bytes = new byte[64];
            var rnd = new Random();
            rnd.NextBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
