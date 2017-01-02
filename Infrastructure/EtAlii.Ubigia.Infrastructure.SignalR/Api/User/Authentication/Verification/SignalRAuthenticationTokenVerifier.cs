namespace EtAlii.Ubigia.Infrastructure.SignalR
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    internal class SignalRAuthenticationTokenVerifier : ISignalRAuthenticationTokenVerifier
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

        public SignalRAuthenticationTokenVerifier(
            IAccountRepository accountRepository,
            IAuthenticationTokenConverter authenticationTokenConverter)
        {
            _accountRepository = accountRepository;
            _authenticationTokenConverter = authenticationTokenConverter;
        }

        public void Verify(string authenticationTokenAsString, string requiredRole)
        {
            var authenticationToken = _authenticationTokenConverter.FromString(authenticationTokenAsString);
            if (authenticationToken != null)
            {
                try
                {
                    var account = _accountRepository.Get(authenticationToken.Name);
                    if (account != null)
                    {
                        // Let's be a bit safe, if the requiredRole is not null we are going to check the roles collection for it.
                        if (requiredRole != null)
                        {
                            if (!account.Roles.Contains(requiredRole))
                            {
                                throw new UnauthorizedInfrastructureOperationException("Unauthorized account: Account does not contain the required role");
                            }
                        }
                        else
                        {
                            // No role is required, just an authenticated user.
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