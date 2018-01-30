namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class SimpleAuthenticationVerifier : ISimpleAuthenticationVerifier
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

        public SimpleAuthenticationVerifier(
            IAccountRepository accountRepository,
            IAuthenticationTokenConverter authenticationTokenConverter)
        {
            _accountRepository = accountRepository;
            _authenticationTokenConverter = authenticationTokenConverter;
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

			var authenticationToken = CreateAuthenticationToken(accountName, hostIdentifier);

            return authenticationToken;
        }

        private string CreateAuthenticationToken(string accountName, string hostIdentifier)
        {
            try
            {
                var success = !String.IsNullOrWhiteSpace(hostIdentifier);
                if (success)
                {
                    var authenticationToken = new AuthenticationToken
                    {
                        Name = accountName,
                        Address = hostIdentifier,
                        Salt = DateTime.UtcNow.ToBinary(),
                    };

                    var authenticationTokenAsBytes = _authenticationTokenConverter.ToBytes(authenticationToken);
                    authenticationTokenAsBytes = Aes.Encrypt(authenticationTokenAsBytes);
                    var authenticationTokenAsString = Convert.ToBase64String(authenticationTokenAsBytes);
                    return authenticationTokenAsString;
                }
                else
                {
                    throw new UnauthorizedInfrastructureOperationException("Invalid identifier");
                }
            }
            catch (Exception e)
            {
                throw new UnauthorizedInfrastructureOperationException("Unauthorized", e);
            }
        }
    }
}