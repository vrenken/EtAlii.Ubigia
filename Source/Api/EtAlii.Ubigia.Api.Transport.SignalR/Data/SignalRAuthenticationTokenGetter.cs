namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System;
    using System.Diagnostics.CodeAnalysis;
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

        [SuppressMessage("Sonar Code Smell", "S2068:Credentials should not be hard-coded", Justification = "Needed to make the downscale from admin/system to user account based authentication tokens")]
        public async Task<string> GetAuthenticationToken(ISignalRSpaceTransport transport, string accountName, string password, string authenticationToken)
        {
            if (password == null && authenticationToken != null)
            {
                // These lines are needed to make the downscale from admin/system to user account based authentication tokens.
                var connection = new HubConnectionFactory().Create(transport, new Uri(transport.Address + UriHelper.Delimiter + SignalRHub.Authentication), authenticationToken);
                await connection.StartAsync().ConfigureAwait(false);
                authenticationToken = await _invoker.Invoke<string>(connection, SignalRHub.Authentication, "AuthenticateAs", accountName, _hostIdentifier).ConfigureAwait(false);
                await connection.DisposeAsync().ConfigureAwait(false);
            }
            else if (password != null && authenticationToken == null)
            {
                var connection = new HubConnectionFactory().CreateForHost(transport, new Uri(transport.Address + UriHelper.Delimiter + SignalRHub.Authentication), _hostIdentifier);
                await connection.StartAsync().ConfigureAwait(false);
                authenticationToken = await _invoker.Invoke<string>(connection, SignalRHub.Authentication, "Authenticate", accountName, password, _hostIdentifier).ConfigureAwait(false);
                await connection.DisposeAsync().ConfigureAwait(false);
            }

            if (string.IsNullOrWhiteSpace(authenticationToken))
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToAthorize);
            }
            return authenticationToken;
        }
       
        [SuppressMessage("Sonar Code Smell", "S2068:Credentials should not be hard-coded", Justification = "Needed to make the downscale from admin/system to user account based authentication tokens")]
        public async Task<string> GetAuthenticationToken(ISignalRStorageTransport transport, string accountName, string password, string authenticationToken)
        {
            if (password == null && authenticationToken != null)
            {
                // These lines are needed to make the downscale from admin/system to user account based authentication tokens.
                var connection = new HubConnectionFactory().Create(transport, new Uri(transport.Address + UriHelper.Delimiter + SignalRHub.Authentication), authenticationToken);
                await connection.StartAsync().ConfigureAwait(false);
                authenticationToken = await _invoker.Invoke<string>(connection, SignalRHub.Authentication, "AuthenticateAs", accountName, _hostIdentifier).ConfigureAwait(false);
                await connection.DisposeAsync().ConfigureAwait(false);
            }
            else if (password != null && authenticationToken == null)
            {
                var connection = new HubConnectionFactory().CreateForHost(transport, new Uri(transport.Address + UriHelper.Delimiter + SignalRHub.Authentication), _hostIdentifier);
                await connection.StartAsync().ConfigureAwait(false);
                authenticationToken = await _invoker.Invoke<string>(connection, SignalRHub.Authentication, "Authenticate", accountName, password, _hostIdentifier).ConfigureAwait(false);
                await connection.DisposeAsync().ConfigureAwait(false);
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
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetNonZeroBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
