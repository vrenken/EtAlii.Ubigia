namespace EtAlii.Servus.Api.Transport.WebApi
{
    using System;
    using System.Threading.Tasks;

    public partial class WebApiAuthenticationDataClient : IAuthenticationDataClient
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
            var address = _connection.AddressFactory.Create(_connection.Storage, RelativeUri.Data.Accounts, UriParameter.AccountName, accountName);
            var account = await _connection.Client.Get<Account>(address);
            if (account == null)
            {
                string message = String.Format("Unable to connect using the specified account ({0})", accountName);
                throw new UnauthorizedInfrastructureOperationException(message);
            }
            return account;
        }
    }
}
