namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using global::Grpc.Core;

    public partial class GrpcAuthenticationDataClient
    {
        private readonly string _hostIdentifier;
        
        public async Task Authenticate(ISpaceConnection connection, string accountName, string password)
        {
            var grpcConnection = (IGrpcSpaceConnection)connection;
             
            SetClients(grpcConnection.Transport.Channel);

            string authenticationToken = null;
            try
            {
                authenticationToken = await GetAuthenticationToken(accountName, password, grpcConnection.Transport.AuthenticationToken);
            }
            catch (global::Grpc.Core.RpcException e)
            {
            }

            if (!String.IsNullOrWhiteSpace(authenticationToken))
            {
                grpcConnection.Transport.AuthenticationToken = authenticationToken;
                grpcConnection.Transport.AuthenticationHeaders = new Metadata { { GrpcHeader.AuthenticationTokenHeaderKey, authenticationToken } };
            }
            else
            {
                grpcConnection.Transport.AuthenticationHeaders = null;
                string message = $"Unable to authenticate on the specified storage ({connection.Transport.Address})";
                throw new UnauthorizedInfrastructureOperationException(message);
            }
        }

        private async Task<string> GetAuthenticationToken(string accountName, string password, string authenticationToken)
        {
	        if (password == null && authenticationToken != null)
	        {
	            var request = new AuthenticationRequest { AccountName = accountName, Password = "", HostIdentifier = _hostIdentifier };
	            var call = _client.AuthenticateAsAsync(request);
	            var response = await call.ResponseAsync
	                .ConfigureAwait(false);
	            _account = response.Account?.ToLocal();
	            
	            var newAuthenticationToken = call
	                .GetTrailers()
	                .Single(header => header.Key == GrpcHeader.AuthenticationTokenHeaderKey).Value;
	            // authenticationToken = await _invoker.Invoke<string>(connection, GrpcHub.Authentication, "AuthenticateAs", accountName, _hostIdentifier);
	            return newAuthenticationToken;
	            
	            //      // These lines are needed to make the downscale from admin/system to user accoun based authentication tokens.
	            //      var connection = new HubConnectionFactory().Create(httpMessageHandler, new Uri(address + GrpcHub.BasePath + "/" + GrpcHub.Authentication), authenticationToken);
	        }
            else if (password != null && authenticationToken == null)
            {
                var request = new AuthenticationRequest { AccountName = accountName, Password = password, HostIdentifier = _hostIdentifier };
                var call = _client.AuthenticateAsync(request);
                var response = await call.ResponseAsync
                    .ConfigureAwait(false);
                _account = response.Account?.ToLocal();
                
                var newAuthenticationToken = call
                    .GetTrailers()
                    .Single(header => header.Key == GrpcHeader.AuthenticationTokenHeaderKey).Value;
                // authenticationToken = await _invoker.Invoke<string>(connection, GrpcHub.Authentication, "Authenticate", accountName, password, _hostIdentifier);
                return newAuthenticationToken;
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
