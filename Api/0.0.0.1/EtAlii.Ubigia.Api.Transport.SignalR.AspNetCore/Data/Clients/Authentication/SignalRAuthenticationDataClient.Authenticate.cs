namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public partial class SignalRAuthenticationDataClient
    {
        private readonly string _hostIdentifier;

        public async Task Authenticate(ISpaceConnection connection)
        {
            var signalRConnection = (ISignalRSpaceConnection)connection;
            var authenticationToken = await GetAuthenticationToken(signalRConnection.Transport.HttpMessageHandler, signalRConnection.Configuration.AccountName, signalRConnection.Configuration.Password, signalRConnection.Configuration.Address, signalRConnection.Transport.AuthenticationToken);
            
            if (!String.IsNullOrWhiteSpace(authenticationToken))
            {
                signalRConnection.Transport.AuthenticationToken = authenticationToken;
            }
            else
            {
                string message = $"Unable to authenticate on the specified storage ({signalRConnection.Configuration.Address})";
                throw new UnauthorizedInfrastructureOperationException(message);
            }
        }

        public async Task Authenticate(IStorageConnection connection)
        {
            var signalRConnection = (ISignalRStorageConnection)connection;
            var authenticationToken = await GetAuthenticationToken(signalRConnection.Transport.HttpMessageHandler, signalRConnection.Configuration.AccountName, signalRConnection.Configuration.Password, signalRConnection.Configuration.Address, signalRConnection.Transport.AuthenticationToken);

            if (!String.IsNullOrWhiteSpace(authenticationToken))
            {
                signalRConnection.Transport.AuthenticationToken = authenticationToken;
            }
            else
            {
                string message = $"Unable to authenticate on the specified storage ({signalRConnection.Configuration.Address})";
                throw new UnauthorizedInfrastructureOperationException(message);
            }
        }

        private async Task<string> GetAuthenticationToken(HttpMessageHandler httpMessageHandler, string accountName, string password, Uri address, string authenticationToken)
        {
	        if (password == null && authenticationToken != null)
	        {
		        // These lines are needed to make the downscale from admin/system to user accoun based authentication tokens.
		        var connection = new HubConnectionFactory().Create(httpMessageHandler, new Uri(address + SignalRHub.BasePath + "/" + SignalRHub.Authentication), authenticationToken);
		        await connection.StartAsync();
		        authenticationToken = await _invoker.Invoke<string>(connection, SignalRHub.Authentication, "AuthenticateAs", accountName, _hostIdentifier);
		        await connection.DisposeAsync();
	        }
            else if (password != null && authenticationToken == null)
            {
				var connection = new HubConnectionFactory().CreateForHost(httpMessageHandler, new Uri(address + SignalRHub.BasePath + "/" + SignalRHub.Authentication), _hostIdentifier);
                await connection.StartAsync();
                authenticationToken = await _invoker.Invoke<string>(connection, SignalRHub.Authentication, "Authenticate", accountName, password, _hostIdentifier);
				await connection.DisposeAsync();
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
