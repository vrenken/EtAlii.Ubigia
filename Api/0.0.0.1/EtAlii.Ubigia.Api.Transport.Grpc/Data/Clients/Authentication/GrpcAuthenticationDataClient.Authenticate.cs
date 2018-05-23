namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;

    public partial class GrpcAuthenticationDataClient
    {
        private readonly string _hostIdentifier;

        public async Task Authenticate(ISpaceConnection connection)
        {
            var grpcConnection = (IGrpcSpaceConnection)connection;
            
            _connection = grpcConnection;
            var channel = grpcConnection.Transport.Channel;
            _client = new AuthenticationGrpcService.AuthenticationGrpcServiceClient(channel);
            
            var authenticationToken = await GetAuthenticationToken(grpcConnection.Configuration.AccountName, grpcConnection.Configuration.Password, grpcConnection.Transport.AuthenticationToken);
            
            if (!String.IsNullOrWhiteSpace(authenticationToken))
            {
                grpcConnection.Transport.AuthenticationToken = authenticationToken;
            }
            else
            {
                string message = $"Unable to authenticate on the specified storage ({grpcConnection.Configuration.Address})";
                throw new UnauthorizedInfrastructureOperationException(message);
            }
        }

        public async Task Authenticate(IStorageConnection connection)
        {
            var grpcConnection = (IGrpcStorageConnection)connection;

            _connection = grpcConnection;
            var channel = grpcConnection.Transport.Channel;
            _client = new AuthenticationGrpcService.AuthenticationGrpcServiceClient(channel);
            
            var authenticationToken = await GetAuthenticationToken(grpcConnection.Configuration.AccountName, grpcConnection.Configuration.Password, grpcConnection.Transport.AuthenticationToken);

            if (!String.IsNullOrWhiteSpace(authenticationToken))
            {
                grpcConnection.Transport.AuthenticationToken = authenticationToken;
            }
            else
            {
                string message = $"Unable to authenticate on the specified storage ({grpcConnection.Configuration.Address})";
                throw new UnauthorizedInfrastructureOperationException(message);
            }
        }

        private async Task<string> GetAuthenticationToken(string accountName, string password, string authenticationToken)
        {
	        if (password == null && authenticationToken != null)
	        {
	            var request = new AuthenticationRequest { AccountName = accountName, Password = password, HostIdentifier = _hostIdentifier };
	            var call = _client.AuthenticateAsAsync(request);
	            await call.ResponseAsync;
	            var newAuthenticationToken = call
	                .GetTrailers()
	                .Single(header => header.Key == GrpcHeader.AuthenticationTokenHeaderKey).Value;
	            return newAuthenticationToken;
	            
	            // DONE: GRPC
	            //      // These lines are needed to make the downscale from admin/system to user accoun based authentication tokens.
	            //      var connection = new HubConnectionFactory().Create(httpMessageHandler, new Uri(address + GrpcHub.BasePath + "/" + GrpcHub.Authentication), authenticationToken);
	            //await connection.StartAsync();
	            //authenticationToken = await _invoker.Invoke<string>(connection, GrpcHub.Authentication, "AuthenticateAs", accountName, _hostIdentifier);
	            //await connection.DisposeAsync();
	        }
            else if (password != null && authenticationToken == null)
            {
                var request = new AuthenticationRequest { AccountName = accountName, Password = password, HostIdentifier = _hostIdentifier };
                var call = _client.AuthenticateAsAsync(request);
                await call.ResponseAsync;
                var newAuthenticationToken = call
                    .GetTrailers()
                    .Single(header => header.Key == GrpcHeader.AuthenticationTokenHeaderKey).Value;
                return newAuthenticationToken;
                
                // DONE: GRPC
                //            var connection = new HubConnectionFactory().CreateForHost(httpMessageHandler, new Uri(address + GrpcHub.BasePath + "/" + GrpcHub.Authentication), _hostIdentifier);
                //            await connection.StartAsync();
                //            authenticationToken = await _invoker.Invoke<string>(connection, GrpcHub.Authentication, "Authenticate", accountName, password, _hostIdentifier);
                //await connection.DisposeAsync();
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
