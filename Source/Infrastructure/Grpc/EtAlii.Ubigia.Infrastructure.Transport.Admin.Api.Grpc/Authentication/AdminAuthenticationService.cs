// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
    using global::Grpc.Core;

    public class AdminAuthenticationService : AuthenticationGrpcService.AuthenticationGrpcServiceBase, IAdminAuthenticationService
    {
        private readonly ISimpleAuthenticationVerifier _authenticationVerifier;
        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;
        private readonly ISimpleAuthenticationBuilder _authenticationBuilder;

        public AdminAuthenticationService(
            ISimpleAuthenticationVerifier authenticationVerifier,
            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier,
            ISimpleAuthenticationBuilder authenticationBuilder)
        {
            _authenticationVerifier = authenticationVerifier;
            _authenticationTokenVerifier = authenticationTokenVerifier;
            _authenticationBuilder = authenticationBuilder;
        }

        public override async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request, ServerCallContext context)
        {
            var (authenticationToken, _) = await _authenticationVerifier
                .Verify(request.AccountName, request.Password, request.HostIdentifier, Role.User, Role.System)
                .ConfigureAwait(false);

            context.ResponseTrailers.Add(GrpcHeader.AuthenticationTokenHeaderKey, authenticationToken);
            var response = new AuthenticationResponse();
            return response;
        }

        public override async Task<AuthenticationResponse> AuthenticateAs(AuthenticationRequest request, ServerCallContext context)
        {
            var currentAccountAuthenticationToken = context.RequestHeaders.Single(h => h.Key == GrpcHeader.AuthenticationTokenHeaderKey).Value;
            await _authenticationTokenVerifier.Verify(currentAccountAuthenticationToken, Role.User, Role.System).ConfigureAwait(false);

            var otherAccountAuthenticationToken = _authenticationBuilder.Build(request.AccountName, request.HostIdentifier);

            context.ResponseTrailers.Add(GrpcHeader.AuthenticationTokenHeaderKey, otherAccountAuthenticationToken);
            var response = new AuthenticationResponse();
            return response;
        }

//        public override Task<LocalStorageResponse> GetLocalStorage(LocalStorageRequest request, ServerCallContext context)
//        [
//            var authenticationToken = context.RequestHeaders.Single(h => h.Key == GrpcHeader.AuthenticationTokenHeaderKey).Value
//            _authenticationTokenVerifier.Verify(authenticationToken, Role.User, Role.System)
//
//            var storage = _storageRepository.GetLocal()
//
//            var response = new LocalStorageResponse
//            [
//                StorageName = storage.Name
//            ]
//            return Task.FromResult(response)
//        ]
    }
}
