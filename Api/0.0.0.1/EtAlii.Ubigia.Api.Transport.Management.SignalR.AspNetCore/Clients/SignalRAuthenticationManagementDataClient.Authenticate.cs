namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.SignalR;

    public partial class SignalRAuthenticationManagementDataClient
    {
        private readonly string _hostIdentifier;

        public async Task Authenticate(IStorageConnection storageConnection, string accountName, string password)
        {
            var signalRConnection = (ISignalRStorageConnection)storageConnection;
            var authenticationToken = await GetAuthenticationToken(signalRConnection.Transport, accountName, password, signalRConnection.Transport.AuthenticationToken);

            if (!String.IsNullOrWhiteSpace(authenticationToken))
            {
                signalRConnection.Transport.AuthenticationToken = authenticationToken;
            }
            else
            {
                string message = $"Unable to authenticate on the specified storage ({storageConnection.Transport.Address})";
                throw new UnauthorizedInfrastructureOperationException(message);
            }
        }

        private async Task<string> GetAuthenticationToken(ISignalRTransport transport, string accountName, string password, string authenticationToken)
        {
	        if (password == null && authenticationToken != null)
	        {
		        // These lines are needed to make the downscale from admin/system to user account based authentication tokens.
		        var connection = new HubConnectionFactory().Create(transport, new Uri(transport.Address + SignalRHub.BasePath + SignalRHub.Authentication), authenticationToken);
		        await connection.StartAsync();
		        authenticationToken = await _invoker.Invoke<string>(connection, SignalRHub.Authentication, "AuthenticateAs", accountName, _hostIdentifier);
		        await connection.DisposeAsync();
	        }
            else if (password != null && authenticationToken == null)
	        {
				var connection = new HubConnectionFactory().CreateForHost(transport, new Uri(transport.Address + SignalRHub.BasePath + SignalRHub.Authentication), _hostIdentifier);
                await connection.StartAsync();
                authenticationToken = await _invoker.Invoke<string>(connection, SignalRHub.Authentication, "Authenticate", accountName, password, _hostIdentifier);
				await connection.DisposeAsync();
            }

            if (String.IsNullOrWhiteSpace(authenticationToken))
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToAthorize);
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
