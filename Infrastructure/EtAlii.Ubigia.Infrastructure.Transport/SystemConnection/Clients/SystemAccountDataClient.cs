﻿namespace EtAlii.Ubigia.Infrastructure.Transport
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

        public Task<Account> Add(string accountName, string accountPassword, AccountTemplate template)
        {
            // TODO: This is where the template functionality should continue.
            var account = new Account
            {
                Name = accountName,
                Password = accountPassword,
            };

            account = _infrastructure.Accounts.Add(account, template);
            return Task.FromResult(account);
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

        public Task<IEnumerable<Account>> GetAll()
        {
            var accounts = _infrastructure.Accounts.GetAll();
            return Task.FromResult(accounts);
        }
    }
}
