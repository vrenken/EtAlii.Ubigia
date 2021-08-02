// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

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

                SetClients(grpcConnection.Transport.CallInvoker);

                var authenticationToken = grpcConnection.Transport.AuthenticationToken;
                if (password == null && authenticationToken == null)
                {
                    throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.NoWayToAuthenticate);
                }

                if (password == null)
                {
                    // Let's add the AuthenticationToken header manually.
                    var metadata = new Metadata { new(GrpcHeader.AuthenticationTokenHeaderKey, authenticationToken) };
                    var request = new AuthenticationRequest { AccountName = accountName, Password = "", HostIdentifier = _hostIdentifier };
                    var call = _client.AuthenticateAsAsync(request, metadata);
                    var response = await call.ResponseAsync.ConfigureAwait(false);
                    _account = response.Account?.ToLocal();

                    authenticationToken = call
                        .GetTrailers()
                        .Single(header => header.Key == GrpcHeader.AuthenticationTokenHeaderKey).Value;
                }
                else if (authenticationToken == null)
                {
                    var metadata = new Metadata();
                    var request = new AuthenticationRequest { AccountName = accountName, Password = password, HostIdentifier = _hostIdentifier };
                    var call = _client.AuthenticateAsync(request, metadata);
                    var response = await call.ResponseAsync.ConfigureAwait(false);
                    _account = response.Account?.ToLocal();

                    authenticationToken = call
                        .GetTrailers()
                        .Single(header => header.Key == GrpcHeader.AuthenticationTokenHeaderKey).Value;
                }

                if (!string.IsNullOrWhiteSpace(authenticationToken))
                {
                    grpcConnection.Transport.AuthenticationToken = authenticationToken;
                    grpcConnection.Transport.AuthenticationHeader = new Metadata.Entry(GrpcHeader.AuthenticationTokenHeaderKey, authenticationToken);
                }
                else
                {
                    grpcConnection.Transport.AuthenticationHeader = null;
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
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetNonZeroBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
