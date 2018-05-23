﻿namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class SimpleAuthenticationTokenVerifier : ISimpleAuthenticationTokenVerifier
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

        public SimpleAuthenticationTokenVerifier(
            IAccountRepository accountRepository,
            IAuthenticationTokenConverter authenticationTokenConverter)
        {
            _accountRepository = accountRepository;
            _authenticationTokenConverter = authenticationTokenConverter;
        }

        
        public void Verify(string authenticationTokenAsString, params string[] requiredRoles)
        {
            Verify(authenticationTokenAsString, out _, requiredRoles);
        }

        public void Verify(string authenticationTokenAsString, out Account account, params string[] requiredRoles)
        {
            var authenticationToken = _authenticationTokenConverter.FromString(authenticationTokenAsString);
            if (authenticationToken != null)
            {
                try
                {
                    account = _accountRepository.Get(authenticationToken.Name);
                    if (account == null)
                    {
	                    throw new UnauthorizedInfrastructureOperationException("Unauthorized account: Account does not contain the required role");
                    }
	                // Let's be a bit safe, if there are any requiredRoles we are going to check the roles collection for it.
	                if (requiredRoles.Any())
	                {
		                var hasOneRequiredRole = account.Roles.Any(role => requiredRoles.Any(requiredRole => requiredRole == role));
		                if (!hasOneRequiredRole)
		                {
			                throw new UnauthorizedInfrastructureOperationException("Invalid role");
		                }
	                }

				}
				catch (Exception e)
                {
                    throw new UnauthorizedInfrastructureOperationException("Unauthorized account", e);
                }
            }
            else
            {
                throw new UnauthorizedInfrastructureOperationException("Missing Authentication-Token");
            }
        }
    }
}