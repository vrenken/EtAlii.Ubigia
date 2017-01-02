namespace EtAlii.Servus.Api.Transport.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class WebApiAuthenticationDataClient : IAuthenticationDataClient
    {
        private IConnection _connection;

        public WebApiAuthenticationDataClient()
        {
        }

        public async Task<Account> GetAccount(string accountName)
        {
            var address = _connection.AddressFactory.Create(_connection.Storage, RelativeUri.Accounts, UriParameter.AccountName, accountName);
            var account = await _connection.Client.Get<Account>(address);
            if (account == null)
            {
                string message = String.Format("Unable to connect using the specified account ({0})", accountName);
                throw new UnauthorizedInfrastructureOperationException(message);
            }
            return account;
        }

        public async Task<Space> GetSpace(Account currentAccount, string spaceName)
        {
            var address = _connection.AddressFactory.Create(_connection.Storage, RelativeUri.Spaces, UriParameter.AccountId, currentAccount.Id.ToString());
            var spaces = await _connection.Client.Get<IEnumerable<Space>>(address);
            return spaces.FirstOrDefault(s => s.Name == spaceName);
        }

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await this.Connect((IConnection) spaceConnection);
        }

        public async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await this.Disconnect((IConnection)spaceConnection);
        }

        public async Task Connect(IConnection connection)
        {
            await Task.Run(() => { _connection = connection; });
        }

        public async Task Disconnect(IConnection connection)
        {
            await Task.Run(() => { _connection = null; });
        }
    }
}
