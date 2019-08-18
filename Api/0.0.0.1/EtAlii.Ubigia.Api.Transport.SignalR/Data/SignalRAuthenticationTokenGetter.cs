namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System;
    using System.Security.Cryptography;
    using System.Threading.Tasks;

    public class SignalRAuthenticationTokenGetter : ISignalRAuthenticationTokenGetter
    {
        private readonly IHubProxyMethodInvoker _invoker;
        private readonly string _hostIdentifier;


        public SignalRAuthenticationTokenGetter(IHubProxyMethodInvoker invoker)
        {
            _invoker = invoker;
            _hostIdentifier = CreateHostIdentifier();
        }

        public async Task<string> GetAuthenticationToken(ISignalRSpaceTransport transport, string accountName, string password, string authenticationToken)
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

            if (string.IsNullOrWhiteSpace(authenticationToken))
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToAthorize);
            }
            return authenticationToken;
        }
       
        public async Task<string> GetAuthenticationToken(ISignalRStorageTransport transport, string accountName, string password, string authenticationToken)
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

            if (string.IsNullOrWhiteSpace(authenticationToken))
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToAthorize);
            }
            return authenticationToken;
        }
        
        private string CreateHostIdentifier()
        {
            var bytes = new byte[64];
            var rnd = RandomNumberGenerator.Create();
            rnd.GetNonZeroBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
