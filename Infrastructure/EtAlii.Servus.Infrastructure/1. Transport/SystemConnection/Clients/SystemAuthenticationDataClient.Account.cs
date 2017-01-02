namespace EtAlii.Servus.Infrastructure.Transport
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;

    internal partial class SystemAuthenticationDataClient : IAuthenticationDataClient
    {
        public async Task<Account> GetAccount(ISpaceConnection connection)
        {
            if (connection.Account != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var account = await GetAccount(connection.Configuration.AccountName);
            if (account == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectUsingAccount);
            }
            return account;
        }

        private async Task<Account> GetAccount(string accountName)
        {
            var account = _infrastructure.Accounts.Get(accountName);
            if (account == null)
            {
                string message = String.Format("Unable to connect using the specified account ({0})", accountName);
                throw new UnauthorizedInfrastructureOperationException(message);
            }
            return await Task.FromResult(account);
        }
    }
}
