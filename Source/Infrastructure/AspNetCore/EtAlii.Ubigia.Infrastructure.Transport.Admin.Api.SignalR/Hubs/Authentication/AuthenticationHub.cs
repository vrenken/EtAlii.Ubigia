// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.SignalR
{
    using Microsoft.AspNetCore.SignalR;

    public class AuthenticationHub : Hub
    {
        private readonly ISimpleAuthenticationVerifier _authenticationVerifier;

        public AuthenticationHub(ISimpleAuthenticationVerifier authenticationVerifier)
        {
            _authenticationVerifier = authenticationVerifier;
        }

        public string Authenticate(string accountName, string password, string hostIdentifier)
        {
            return _authenticationVerifier.Verify(accountName, password, hostIdentifier, Role.Admin, Role.System);
        }
    }
}
