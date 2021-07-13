// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
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
            // Improve the account templating functionality by converting initialization to a script based approach.
            // This is where the template functionality should continue.
            // More details can be found in the Github issue below:
            // https://github.com/vrenken/EtAlii.Ubigia/issues/96
            var account = new Account
            {
                Name = accountName,
                Password = accountPassword,
            };

            account = await _infrastructure.Accounts
                .Add(account, template)
                .ConfigureAwait(false);
            return account;
        }

        public Task Remove(Guid accountId)
        {
            _infrastructure.Accounts.Remove(accountId);
            return Task.CompletedTask;
        }

        public Task<Account> Change(Guid accountId, string accountName, string accountPassword)
        {
            var account = new Account
            {
                Id = accountId,
                Name = accountName,
                Password = accountPassword,
            };
            account = _infrastructure.Accounts.Update(accountId, account);
            return Task.FromResult(account);
        }

        public Task<Account> Change(Account account)
        {
            account = _infrastructure.Accounts.Update(account.Id, account);
            return Task.FromResult(account);
        }

        public Task<Account> Get(string accountName)
        {
            var account = _infrastructure.Accounts.Get(accountName);
            return Task.FromResult(account);
        }

        public Task<Account> Get(Guid accountId)
        {
            var account = _infrastructure.Accounts.Get(accountId);
            return Task.FromResult(account);
        }

        public IAsyncEnumerable<Account> GetAll()
        {
            return _infrastructure.Accounts.GetAll();
        }
    }
}
