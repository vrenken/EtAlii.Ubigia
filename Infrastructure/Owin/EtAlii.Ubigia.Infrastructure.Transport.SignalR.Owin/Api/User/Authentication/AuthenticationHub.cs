namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.SignalR
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.AspNet.SignalR;

    public class AuthenticationHub : Hub
    {
        private readonly IStorageRepository _storageRepository;

        private readonly ISimpleAuthenticationVerifier _authenticationVerifier;
        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;

        public AuthenticationHub(
            ISimpleAuthenticationVerifier authenticationVerifier,
            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier, 
            IStorageRepository storageRepository)
        {
            _authenticationVerifier = authenticationVerifier;
            _authenticationTokenVerifier = authenticationTokenVerifier;
            _storageRepository = storageRepository;
        }

        public string Authenticate(string accountName, string password, string hostIdentifier)
        {
            return _authenticationVerifier.Verify(accountName, password, hostIdentifier);
        }

        public Storage GetLocalStorage()
        {
            var authenticationToken = Context.Headers.Get("Authentication-Token");
            _authenticationTokenVerifier.Verify(authenticationToken);

            return _storageRepository.GetLocal();
        }
    }
}
