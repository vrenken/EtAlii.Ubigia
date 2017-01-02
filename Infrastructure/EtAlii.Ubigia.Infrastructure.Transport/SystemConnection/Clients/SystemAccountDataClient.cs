namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    internal class SystemAccountDataClient : SystemStorageClientBase, IAccountDataClient
    {
        private readonly IInfrastructure _infrastructure;

        public SystemAccountDataClient(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }

        public async Task<Account> Add(string accountName, string accountPassword, AccountTemplate template)
        {
            // TODO: This is where the template functionality should continue.
            var account = new Account
            {
                Name = accountName,
                Password = accountPassword,
            };

            account = _infrastructure.Accounts.Add(account, template);
            return await Task.FromResult(account);
        }

        public async Task Remove(Guid accountId)
        {
            await Task.Run(() => _infrastructure.Accounts.Remove(accountId));
        }

        public async Task<Account> Change(Guid accountId, string accountName, string accountPassword)
        {
            var account = new Account
            {
                Id = accountId,
                Name = accountName,
                Password = accountPassword,
            };
            account = _infrastructure.Accounts.Update(accountId, account);
            return await Task.FromResult(account);
        }

        public async Task<Account> Change(Account account)
        {
            account = _infrastructure.Accounts.Update(account.Id, account);
            return await Task.FromResult(account);
        }

        public async Task<Account> Get(string accountName)
        {
            var account = _infrastructure.Accounts.Get(accountName);
            return await Task.FromResult(account);
        }

        public async Task<Account> Get(Guid accountId)
        {
            var account = _infrastructure.Accounts.Get(accountId);
            return await Task.FromResult(account);
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            var accounts = _infrastructure.Accounts.GetAll();
            return await Task.FromResult(accounts);
        }
    }
}
