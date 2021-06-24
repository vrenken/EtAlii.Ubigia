// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Logical;

    internal class AccountRepository : IAccountRepository
    {
        private readonly ILogicalContext _logicalContext;
        private readonly IAccountInitializer _accountInitializer;

		public AccountRepository(
            ILogicalContext logicalContext, 
            IAccountInitializer accountInitializer)
        {
            _accountInitializer = accountInitializer;
			_logicalContext = logicalContext;
        }

        public IAsyncEnumerable<Account> GetAll()
        {
            return _logicalContext.Accounts.GetAll();
        }

		public Account Get(string accountName)
		{
			return _logicalContext.Accounts.Get(accountName);
		}

		public Account Get(string accountName, string password)
        {
            return _logicalContext.Accounts.Get(accountName, password);
        }

        public Account Get(Guid itemId)
        {
            return _logicalContext.Accounts.Get(itemId);
        }


        public Account Update(Guid itemId, Account item)
        {
            return _logicalContext.Accounts.Update(itemId, item);
        }

        public async Task<Account> Add(Account item, AccountTemplate template)
        {
            var now = DateTime.UtcNow;
            item.Created = now;
            // Nope. we are not updating so we set the updated value to null.
            item.Updated = null;

            var addedAccount = _logicalContext.Accounts.Add(item, template, out var isAdded);
            if (isAdded)
            {
                await _accountInitializer.Initialize(addedAccount, template).ConfigureAwait(false);
            }

            return addedAccount;
        }

        public void Remove(Guid itemId)
        {
            _logicalContext.Accounts.Remove(itemId);
        }

        public void Remove(Account item)
        {
            _logicalContext.Accounts.Remove(item);
        }
    }
}