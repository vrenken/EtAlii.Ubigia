// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using global::Grpc.Core;

    public class UserAuthenticationService : AuthenticationGrpcService.AuthenticationGrpcServiceBase, IUserAuthenticationService
    {
        private readonly ISimpleAuthenticationVerifier _authenticationVerifier;
        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;
	    private readonly ISimpleAuthenticationBuilder _authenticationBuilder;

		public UserAuthenticationService(
            ISimpleAuthenticationVerifier authenticationVerifier,
            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier, 
            ISimpleAuthenticationBuilder authenticationBuilder)
        {
            _authenticationVerifier = authenticationVerifier;
            _authenticationTokenVerifier = authenticationTokenVerifier;
	        _authenticationBuilder = authenticationBuilder;
        }

        public override Task<AuthenticationResponse> Authenticate(AuthenticationRequest request, ServerCallContext context)
        {
            var authenticationToken = _authenticationVerifier.Verify(request.AccountName, request.Password, request.HostIdentifier, out var account, Role.User, Role.System);
            
            context.ResponseTrailers.Add(GrpcHeader.AuthenticationTokenHeaderKey, authenticationToken);
            var response = new AuthenticationResponse { Account = account.ToWire() };

            // We do not want to return the password.
            response.Account.Password = "";
            
            return Task.FromResult(response);
        }

        public override Task<AuthenticationResponse> AuthenticateAs(AuthenticationRequest request, ServerCallContext context)
        {
            // First we authenticate as the current user (most probably the administrator).
            var currentAccountAuthenticationToken = context.RequestHeaders.Single(header => header.Key == GrpcHeader.AuthenticationTokenHeaderKey).Value;
            _authenticationTokenVerifier.Verify(currentAccountAuthenticationToken, out var account, Role.User, Role.System);

            // And next we authenticate as the other user (the user we want to authenticate as).
            var otherAccountAuthenticationToken = _authenticationBuilder.Build(request.AccountName, request.HostIdentifier);
            _authenticationTokenVerifier.Verify(otherAccountAuthenticationToken, out account, Role.User);

            context.ResponseTrailers.Add(GrpcHeader.AuthenticationTokenHeaderKey, otherAccountAuthenticationToken);
            var response = new AuthenticationResponse { Account = account.ToWire() };
            
            // We do not want to return the password.
            response.Account.Password = "";
            
            return Task.FromResult(response);
        }
//
//        public override Task<LocalStorageResponse> GetLocalStorage(LocalStorageRequest request, ServerCallContext context)
//        [
//            var currentAccountAuthenticationToken = context.RequestHeaders.Single(header => header.Key == GrpcHeader.AuthenticationTokenHeaderKey).Value
//            _authenticationTokenVerifier.Verify(currentAccountAuthenticationToken, Role.User, Role.System)
//
//            var storage = _storageRepository.GetLocal()
//
//            var response = new LocalStorageResponse
//            [
//                Storage = storage.ToWire()
//            ]
//            return Task.FromResult(response)
//        ]
    }
}
