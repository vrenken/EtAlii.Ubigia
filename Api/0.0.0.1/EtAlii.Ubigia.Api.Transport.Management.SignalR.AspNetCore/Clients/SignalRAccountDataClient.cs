namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using Microsoft.AspNetCore.SignalR.Client;

    public class SignalRAccountDataClient : IAccountDataClient<ISignalRStorageTransport>
    {
        private HubConnection _connection;

        private readonly IHubProxyMethodInvoker _invoker;

        public SignalRAccountDataClient(IHubProxyMethodInvoker invoker)
        {
            _invoker = invoker;
        }

        public async Task<Account> Add(string accountName, string accountPassword, AccountTemplate template)
        {
            var account = new Account
            {
                Name = accountName,
                Password = accountPassword,
            };

            return await _invoker.Invoke<Account>(_connection, SignalRHub.Account, "Post", account, template.Name);
        }

        public async Task Remove(Guid accountId)
        {
            await _invoker.Invoke(_connection, SignalRHub.Account, "Delete", accountId);
        }

        public async Task<Account> Change(Guid accountId, string accountName, string accountPassword)
        {
            var account = new Account
            {
                Id = accountId,
                Name = accountName,
                Password = accountPassword,
            };

            return await _invoker.Invoke<Account>(_connection, SignalRHub.Account, "Put", accountId, account);
        }

        public async Task<Account> Change(Account account)
        {
            return await _invoker.Invoke<Account>(_connection, SignalRHub.Account, "Put", account.Id, account);
        }

        public async Task<Account> Get(string accountName)
        {
            return await _invoker.Invoke<Account>(_connection, SignalRHub.Account, "GetByName", accountName);
        }

        public async Task<Account> Get(Guid accountId)
        {
            return await _invoker.Invoke<Account>(_connection, SignalRHub.Account, "Get", accountId);
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _invoker.Invoke<IEnumerable<Account>>(_connection, SignalRHub.Account, "GetAll");
        }

        public async Task Connect(IStorageConnection storageConnection)
        {
            await Connect((IStorageConnection<ISignalRStorageTransport>) storageConnection);
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Disconnect((IStorageConnection<ISignalRStorageTransport>)storageConnection);
        }

        public async Task Connect(IStorageConnection<ISignalRStorageTransport> storageConnection)
        {
            _connection = new HubConnectionFactory().Create(storageConnection.Transport, new Uri(storageConnection.Storage.Address + SignalRHub.BasePath + UriConstant.PathSeparator + SignalRHub.Account, UriKind.Absolute));
			await _connection.StartAsync();
        }

        public async Task Disconnect(IStorageConnection<ISignalRStorageTransport> storageConnection)
        {
            await _connection.DisposeAsync();
            _connection = null;
        }
    }
}
