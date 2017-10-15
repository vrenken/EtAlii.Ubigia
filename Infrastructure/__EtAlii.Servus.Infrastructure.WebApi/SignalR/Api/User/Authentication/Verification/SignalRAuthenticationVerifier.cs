namespace EtAlii.Servus.Infrastructure.SignalR
{
    using System;
    using EtAlii.Servus.Api.Transport;

    internal class SignalRAuthenticationVerifier : ISignalRAuthenticationVerifier
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IInfrastructureConfiguration _configuration;
        private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

        public SignalRAuthenticationVerifier(
            IAccountRepository accountRepository,
            IInfrastructureConfiguration configuration,
            IAuthenticationTokenConverter authenticationTokenConverter)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
            _authenticationTokenConverter = authenticationTokenConverter;
        }

        public string Verify(string accountName, string password, string hostIdentifier)
        {
            if (String.IsNullOrWhiteSpace(accountName) || String.IsNullOrWhiteSpace(password))
            {
                throw new InvalidInfrastructureOperationException("Unauthorized");
            }

            var isAdmin = _configuration.Account == accountName && _configuration.Password == password;

            var account = _accountRepository.Get(accountName, password);
            if (account == null && !isAdmin)
            {
                throw new UnauthorizedInfrastructureOperationException("Invalid account");
            }
            //var accountName = isAdmin ? _configuration.Account : account.Name;

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