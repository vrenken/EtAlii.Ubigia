namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System.Threading.Tasks;

    public partial class SignalRAuthenticationDataClient : SignalRClientBase, IAuthenticationDataClient<ISignalRSpaceTransport>
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
            var account = await _invoker.Invoke<Account>(_accountProxy, SignalRHub.Account, "GetByName", accountName);
            if (account == null)
            {
                string message = $"Unable to connect using the specified account ({accountName})";
                throw new UnauthorizedInfrastructureOperationException(message);
            }
            return account;
        }

    }
}
