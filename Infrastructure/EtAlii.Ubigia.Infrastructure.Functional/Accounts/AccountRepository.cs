namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
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
            _logicalContext.Accounts.Added += OnAccountAdded;
        }

        private void OnAccountAdded(object sender, AccountAddedEventArgs e)
        {
            _accountInitializer.Initialize(e.Account, e.Template);
        }

        public Account Get(string accountName)
        {
            return _logicalContext.Accounts.Get(accountName);
        }

        public Account Get(string accountName, string password)
        {
            return _logicalContext.Accounts.Get(accountName, password);
        }

        public IEnumerable<Account> GetAll()
        {
            return _logicalContext.Accounts.GetAll();
        }

        public Account Get(Guid id)
        {
            return _logicalContext.Accounts.Get(id);
        }


        public Account Update(Guid itemId, Account updatedItem)
        {
            return _logicalContext.Accounts.Update(itemId, updatedItem);
        }

        public Account Add(Account item, AccountTemplate template)
        {
            var now = DateTime.UtcNow;
            item.Created = now;
            // Nope. we are not updating so we set the updated value to null.
            item.Updated = null;

            item = _logicalContext.Accounts.Add(item, template);

            return item;
        }

        public void Remove(Guid itemId)
        {
            _logicalContext.Accounts.Remove(itemId);
        }

        public void Remove(Account itemToRemove)
        {
            _logicalContext.Accounts.Remove(itemToRemove);
        }
    }
}