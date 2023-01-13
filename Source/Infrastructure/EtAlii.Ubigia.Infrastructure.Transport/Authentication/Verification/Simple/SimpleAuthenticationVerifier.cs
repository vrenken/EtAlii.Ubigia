// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport;

using System.Linq;
using System.Threading.Tasks;
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

    /// <inheritdoc />
    public async Task<(string, Account)> Verify(string accountName, string password, string hostIdentifier, params string[] requiredRoles)
    {
        if (string.IsNullOrWhiteSpace(accountName) || string.IsNullOrWhiteSpace(password))
        {
            throw new InvalidInfrastructureOperationException("Unauthorized");
        }

        var account = await _accountRepository.Get(accountName, password).ConfigureAwait(false);
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

        return (authenticationToken, account);
    }
}
