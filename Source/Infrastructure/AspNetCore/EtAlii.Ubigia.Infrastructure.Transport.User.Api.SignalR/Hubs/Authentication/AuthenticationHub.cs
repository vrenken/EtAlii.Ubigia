// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR
{
	using System.Linq;
	using EtAlii.Ubigia.Infrastructure.Functional;
	using Microsoft.AspNetCore.SignalR;

	public class AuthenticationHub : Hub
    {
        private readonly IStorageRepository _storageRepository;

        private readonly ISimpleAuthenticationVerifier _authenticationVerifier;
        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;
	    private readonly ISimpleAuthenticationBuilder _authenticationBuilder;

		public AuthenticationHub(
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

	    public string Authenticate(string accountName, string password, string hostIdentifier)
	    {
		    return _authenticationVerifier.Verify(accountName, password, hostIdentifier, Role.User, Role.System);
	    }

	    public string AuthenticateAs(string accountName, string hostIdentifier)
	    {
		    Context.GetHttpContext().Request.Headers.TryGetValue("Authentication-Token", out var stringValues);
		    var authenticationToken = stringValues.Single();
		    _authenticationTokenVerifier.Verify(authenticationToken, Role.User, Role.System);

		    return _authenticationBuilder.Build(accountName, hostIdentifier);
		}

		public Storage GetLocalStorage()
        {
            Context.GetHttpContext().Request.Headers.TryGetValue("Authentication-Token", out var stringValues);
            var authenticationToken = stringValues.Single();
            _authenticationTokenVerifier.Verify(authenticationToken, Role.User, Role.System);

            return _storageRepository.GetLocal();
        }
    }
}
