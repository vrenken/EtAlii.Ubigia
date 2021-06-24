// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
    using global::Grpc.Core;

    public partial class GrpcAuthenticationManagementDataClient
    {
        private readonly string _hostIdentifier;

        public async Task Authenticate(IStorageConnection storageConnection, string accountName, string password)
        {
            try
            {
                var grpcConnection = (IGrpcStorageConnection)storageConnection;
                SetClients(grpcConnection.Transport.CallInvoker);

                var authenticationToken = grpcConnection.Transport.AuthenticationToken;
                if (password == null && authenticationToken == null)
                {
                    throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.NoWayToAuthenticate);
                }

                if (password == null)
                {
                    var metadata = Metadata.Empty;
                    var request = new AuthenticationRequest { AccountName = accountName, Password = null, HostIdentifier = _hostIdentifier };
                    var call = _client.AuthenticateAsAsync(request, metadata);
                    await call.ResponseAsync.ConfigureAwait(false);
                    //var response = await call.ResponseAsync
                    //_account = response.Account?.ToLocal()

                    authenticationToken = call
                        .GetTrailers()
                        .Single(header => header.Key == GrpcHeader.AuthenticationTokenHeaderKey).Value;
                }
                if (authenticationToken == null)
                {
                    var metadata = Metadata.Empty;
                    var request = new AuthenticationRequest { AccountName = accountName, Password = password, HostIdentifier = _hostIdentifier };
                    var call = _client.AuthenticateAsync(request, metadata);
                    await call.ResponseAsync.ConfigureAwait(false);
                    //var response = await call.ResponseAsync
                    //_account = response.Account?.ToLocal()

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
                    var message = $"Unable to authenticate on the specified storage ({storageConnection.Transport.Address})";
                    throw new UnauthorizedInfrastructureOperationException(message);
                }
            }
            catch (RpcException e)
            {
                var message = $"Unable to authenticate on the specified storage ({storageConnection.Transport.Address})";
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
