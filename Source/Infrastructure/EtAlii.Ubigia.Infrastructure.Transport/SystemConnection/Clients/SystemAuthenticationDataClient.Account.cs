// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    internal partial class SystemAuthenticationDataClient
    {
        public async Task<Account> GetAccount(ISpaceConnection connection, string accountName)
        {
            if (connection.Account != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var account = await GetAccount(accountName).ConfigureAwait(false);
            if (account == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectUsingAccount);
            }
            return account;
        }

        private Task<Account> GetAccount(string accountName)
        {
            var account = _infrastructure.Accounts.Get(accountName);
            if (account == null)
            {
                var message = $"Unable to connect using the specified account ({accountName})";
                throw new UnauthorizedInfrastructureOperationException(message);
            }
            return Task.FromResult(account);
        }
    }
}
