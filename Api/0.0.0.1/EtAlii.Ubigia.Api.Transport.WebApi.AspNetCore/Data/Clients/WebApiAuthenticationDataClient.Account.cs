namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System.Threading.Tasks;

    public partial class WebApiAuthenticationDataClient
    {
        public async Task<Account> GetAccount(ISpaceConnection connection, string accountName)
        {
            if (connection.Account != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var account = await GetAccount(accountName);
            if (account == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectUsingAccount);
            }
            return account;
        }

        private async Task<Account> GetAccount(string accountName)
        {
            var address = _connection.AddressFactory.Create(_connection.Storage, RelativeUri.Data.Accounts, UriParameter.AccountName, accountName, UriParameter.AuthenticationToken);
            var account = await _connection.Client.Get<Account>(address);
            if (account == null)
            {
                string message = $"Unable to connect using the specified account ({accountName})";
                throw new UnauthorizedInfrastructureOperationException(message);
            }
            return account;
        }
    }
}
