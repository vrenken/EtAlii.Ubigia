// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using System.Threading.Tasks;

    public partial class RestAuthenticationDataClient
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

        private async Task<Account> GetAccount(string accountName)
        {
            var address = _connection.AddressFactory.Create(_connection.Transport, RelativeDataUri.Accounts, UriParameter.AccountName, accountName, UriParameter.AuthenticationToken);
            var account = await _connection.Client.Get<Account>(address).ConfigureAwait(false);
            if (account == null)
            {
                var message = $"Unable to connect using the specified account ({accountName})";
                throw new UnauthorizedInfrastructureOperationException(message);
            }
            return account;
        }
    }
}
