namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class SimpleAuthenticationVerifier : ISimpleAuthenticationVerifier
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ISimpleAuthenticationBuilder _authenticationBuilder;

        public SimpleAuthenticationVerifier(
            IAccountRepository accountRepository,
            ISimpleAuthenticationBuilder authenticationBuilder)
        {
            _accountRepository = accountRepository;
	        _authenticationBuilder = authenticationBuilder;
        }

        public string Verify(string accountName, string password, string hostIdentifier, params string[] requiredRoles)
        {
            if (String.IsNullOrWhiteSpace(accountName) || String.IsNullOrWhiteSpace(password))
            {
                throw new InvalidInfrastructureOperationException("Unauthorized");
            }

            var account = _accountRepository.Get(accountName, password);
            if (account == null)
            {
                throw new UnauthorizedInfrastructureOperationException("Invalid account");
            }

	        if (requiredRoles.Any())
	        {
		        var hasOneRequiredRole = account.Roles.Any(role => requiredRoles.Any(requiredRole => requiredRole == role));
		        if (!hasOneRequiredRole)
		        {
			        throw new UnauthorizedInfrastructureOperationException("Invalid role");
		        }
	        }

			var authenticationToken = _authenticationBuilder.Build(accountName, hostIdentifier);

            return authenticationToken;
        }
    }
}