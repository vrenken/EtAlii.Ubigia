namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
    using System;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.SignalR;

    public partial class SignalRAuthenticationManagementDataClient
    {
        private readonly string _hostIdentifier;
        private readonly RandomNumberGenerator _random;

        public async Task Authenticate(IStorageConnection connection, string accountName, string password)
        {
            var signalRConnection = (ISignalRStorageConnection)connection;
            var authenticationToken = await GetAuthenticationToken(signalRConnection.Transport, accountName, password, signalRConnection.Transport.AuthenticationToken);

            if (!String.IsNullOrWhiteSpace(authenticationToken))
            {
                signalRConnection.Transport.AuthenticationToken = authenticationToken;
            }
            else
            {
                string message = $"Unable to authenticate on the specified storage ({connection.Transport.Address})";
                throw new UnauthorizedInfrastructureOperationException(message);
            }
        }

        private async Task<string> GetAuthenticationToken(ISignalRTransport transport, string accountName, string password, string authenticationToken)
        {
	        if (password == null && authenticationToken != null)
	        {
		        // These lines are needed to make the downscale from admin/system to user account based authentication tokens.
		        var connection = new HubConnectionFactory().Create(transport, new Uri(transport.Address + SignalRHub.BasePath + UriConstant.PathSeparator + SignalRHub.Authentication), authenticationToken);
		        await connection.StartAsync();
		        authenticationToken = await _invoker.Invoke<string>(connection, SignalRHub.Authentication, "AuthenticateAs", accountName, _hostIdentifier);
		        await connection.DisposeAsync();
	        }
            else if (password != null && authenticationToken == null)
	        {
				var connection = new HubConnectionFactory().CreateForHost(transport, new Uri(transport.Address + SignalRHub.BasePath + UriConstant.PathSeparator + SignalRHub.Authentication), _hostIdentifier);
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
            _random.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
