// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    internal class SystemAccountDataClient : SystemStorageClientBase, IAccountDataClient
    {
        private readonly IFunctionalContext _functionalContext;

        public SystemAccountDataClient(IFunctionalContext functionalContext)
        {
            _functionalContext = functionalContext;
        }

        /// <inheritdoc />
        public Task<Account> Add(string accountName, string accountPassword, AccountTemplate template)
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

            return _functionalContext.Accounts.Add(account, template);
        }

        /// <inheritdoc />
        public Task Remove(Guid accountId)
        {
            return _functionalContext.Accounts.Remove(accountId);
        }

        /// <inheritdoc />
        public Task<Account> Change(Guid accountId, string accountName, string accountPassword)
        {
            var account = new Account
            {
                Id = accountId,
                Name = accountName,
                Password = accountPassword,
            };
            return _functionalContext.Accounts.Update(accountId, account);
        }

        /// <inheritdoc />
        public Task<Account> Change(Account account)
        {
            return _functionalContext.Accounts.Update(account.Id, account);
        }

        /// <inheritdoc />
        public Task<Account> Get(string accountName)
        {
            return _functionalContext.Accounts.Get(accountName);
        }

        /// <inheritdoc />
        public Task<Account> Get(Guid accountId)
        {
            return _functionalContext.Accounts.Get(accountId);
        }

        /// <inheritdoc />
        public IAsyncEnumerable<Account> GetAll()
        {
            return _functionalContext.Accounts.GetAll();
        }
    }
}
