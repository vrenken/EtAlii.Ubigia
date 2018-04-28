namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;

    public class AuthenticationService : EtAlii.Ubigia.Api.Transport.Grpc.AuthenticationGrpcService.AuthenticationGrpcServiceBase
    {
        private readonly IStorageRepository _storageRepository;

        private readonly ISimpleAuthenticationVerifier _authenticationVerifier;
        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;
	    private readonly ISimpleAuthenticationBuilder _authenticationBuilder;

		public AuthenticationService(
            ISimpleAuthenticationVerifier authenticationVerifier,
            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier, 
            IStorageRepository storageRepository, 
            ISimpleAuthenticationBuilder authenticationBuilder)
        {
            _authenticationVerifier = authenticationVerifier;
            _authenticationTokenVerifier = authenticationTokenVerifier;
            _storageRepository = storageRepository;
	        _authenticationBuilder = authenticationBuilder;
        }

        public override Task<AuthenticationResponse> Authenticate(AuthenticationRequest request, ServerCallContext context)
        {
            var authenticationToken = _authenticationVerifier.Verify(request.AccountName, request.Password, request.HostIdentifier, Role.User, Role.System);

            var response = new AuthenticationResponse
            {
                Account = null,
                AuthenticationToken = authenticationToken
            };
            return Task.FromResult(response);
        }

        public override Task<AuthenticationResponse> AuthenticateAs(AuthenticationRequest request, ServerCallContext context)
        {
            string currentAccountAuthenticationToken = null;
            
            //Context.Connection.GetHttpContext().Request.Headers.TryGetValue("Authentication-Token", out StringValues stringValues);
            //var authenticationToken = stringValues.Single();
            _authenticationTokenVerifier.Verify(currentAccountAuthenticationToken, Role.User, Role.System);

            var otherAccountAuthenticationToken = _authenticationBuilder.Build(request.AccountName, request.HostIdentifier);

            var response = new AuthenticationResponse
            {
                Account = null,
                AuthenticationToken = otherAccountAuthenticationToken
            };
            return Task.FromResult(response);
        }

        public override Task<AuthenticationTokenResponse> GetAccountForAuthenticationToken(AuthenticationTokenRequest request, ServerCallContext context)
        {
            return base.GetAccountForAuthenticationToken(request, context);
        }

        public override Task<LocalStorageResponse> GetLocalStorage(LocalStorageRequest request, ServerCallContext context)
        {
            string authenticationToken = null;

            //Context.Connection.GetHttpContext().Request.Headers.TryGetValue("Authentication-Token", out StringValues stringValues);
            //var authenticationToken = stringValues.Single();
            _authenticationTokenVerifier.Verify(authenticationToken, Role.User, Role.System);

            var storage = _storageRepository.GetLocal();

            var response = new LocalStorageResponse
            {
                StorageName = storage.Name
            };
            return Task.FromResult(response);
        }
    }
}
