namespace EtAlii.Ubigia.Infrastructure.Transport.SignalR
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.AspNet.SignalR;

    public class AuthenticationHub : Hub
    {
        private readonly IStorageRepository _storageRepository;

        private readonly ISignalRAuthenticationVerifier _authenticationVerifier;
        private readonly ISignalRAuthenticationTokenVerifier _authenticationTokenVerifier;

        public AuthenticationHub(
            ISignalRAuthenticationVerifier authenticationVerifier, 
            ISignalRAuthenticationTokenVerifier authenticationTokenVerifier, 
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
            var authenticationToken = this.Context.Headers.Get("Authentication-Token");
            _authenticationTokenVerifier.Verify(authenticationToken, null);

            return _storageRepository.GetLocal();
        }
    }
}
