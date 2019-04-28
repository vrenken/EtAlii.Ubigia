namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using global::Grpc.Core;

    public partial class GrpcAuthenticationDataClient
    {
        private readonly string _hostIdentifier;
        
        public async Task Authenticate(ISpaceConnection connection, string accountName, string password)
        {
            try
            {
                var grpcConnection = (IGrpcSpaceConnection)connection;
                 
                SetClients(grpcConnection.Transport.Channel);
    
                var authenticationToken = grpcConnection.Transport.AuthenticationToken;
                if (password == null && authenticationToken == null)
                {
                    throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.NoWayToAuthenticate);
                }
                
                if (password == null)
                {
                    var request = new AuthenticationRequest { AccountName = accountName, Password = "", HostIdentifier = _hostIdentifier };
                    // Let's add the AuthenticationToken header manually.
                    var call = _client.AuthenticateAsAsync(request, new Metadata { { GrpcHeader.AuthenticationTokenHeaderKey, authenticationToken } });
                    var response = await call.ResponseAsync;
                    _account = response.Account?.ToLocal();
                    
                    authenticationToken = call
                        .GetTrailers()
                        .Single(header => header.Key == GrpcHeader.AuthenticationTokenHeaderKey).Value;
                }
                else if (authenticationToken == null)
                {
                    var request = new AuthenticationRequest { AccountName = accountName, Password = password, HostIdentifier = _hostIdentifier };
                    var call = _client.AuthenticateAsync(request);
                    var response = await call.ResponseAsync;
                    _account = response.Account?.ToLocal();
                    
                    authenticationToken = call
                        .GetTrailers()
                        .Single(header => header.Key == GrpcHeader.AuthenticationTokenHeaderKey).Value;
                }
    
                if (!String.IsNullOrWhiteSpace(authenticationToken))
                {
                    grpcConnection.Transport.AuthenticationToken = authenticationToken;
                    grpcConnection.Transport.AuthenticationHeaders = new Metadata { { GrpcHeader.AuthenticationTokenHeaderKey, authenticationToken } };
                }
                else
                {
                    grpcConnection.Transport.AuthenticationHeaders = null;
                    var message = $"Unable to authenticate on the specified space ({connection.Transport.Address} {connection.Configuration.Space ?? "[NONE]"})";
                    throw new UnauthorizedInfrastructureOperationException(message);
                }
            }
            catch (RpcException e)
            {                
                var message = $"Unable to authenticate on the specified space ({connection.Transport.Address} {connection.Configuration.Space ?? "[NONE]"})";
                throw new UnauthorizedInfrastructureOperationException(message, e);
            }
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
