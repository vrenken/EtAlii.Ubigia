namespace EtAlii.Servus.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Infrastructure.Fabric;

    public class LogicalAccountSet : ILogicalAccountSet
    {
        private readonly IFabricContext _fabric;
        private readonly IAccountInitializer _accountInitializer;
        private readonly object _lockObject = new object();

        private const string _folder = "Accounts";

        private ObservableCollection<Account> Items { get { lock (_lockObject) { return _items ?? (_items = InitializeItems()); } } }
        private ObservableCollection<Account> _items; // We don't us a Lazy construction here because the first get of this property is actually cascaded through the logical layer. A Lazy instance results in a deadlock.

        public LogicalAccountSet(
            IAccountInitializer accountInitializer, 
            IFabricContext fabric)
        {
            _fabric = fabric;
            _accountInitializer = accountInitializer;
        }

        public Account Get(string accountName)
        {
            return Items.SingleOrDefault(account => account.Name == accountName);
        }

        public Account Get(string accountName, string password)
        {
            return Items.SingleOrDefault(account => account.Name == accountName && account.Password == password);
        }

        private bool CannAddFunction(IList<Account> items, Account item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("No item specified");
            }

            var canAdd = !String.IsNullOrWhiteSpace(item.Name);
            if (canAdd)
            {
                canAdd = item.Id == Guid.Empty;
            }
            if (canAdd)
            {
                canAdd = !items.Any(i => String.CompareOrdinal(i.Name, item.Name) == 0 || i.Id == item.Id);
            }
            return canAdd;
        }

        private Account UpdateFunction(Account originalItem, Account updatedItem)
        {
            originalItem.Name = updatedItem.Name;
            originalItem.Password = updatedItem.Password;
            originalItem.Roles = updatedItem.Roles;

            // We should not update the created date of the account.
            //originalItem.Created = updatedItem.Created;

            // Whenever we touch the account we also register the updated time.
            originalItem.Updated = DateTime.UtcNow;

            return originalItem;
        }


        private ObservableCollection<Account> InitializeItems()
        {
            var items = _fabric.Items.GetItems<Account>(_folder);
            return items;
        }

        public Account Add(Account item, AccountTemplate template)
        {
            var account = _fabric.Items.Add(Items, CannAddFunction, item);

            if (account != null)
            {
                _accountInitializer.Initialize(account, template);
            }
            return account;
        }

        public IEnumerable<Account> GetAll()
        {
            return _fabric.Items.GetAll(Items);
        }

        public Account Get(Guid id)
        {
            return _fabric.Items.Get(Items, id);
        }

        public ObservableCollection<Account> GetItems()
        {
            return _fabric.Items.GetItems<Account>(_folder);
        }

        public void Remove(Guid itemId)
        {
            _fabric.Items.Remove<Account>(Items, itemId);
        }

        public void Remove(Account itemToRemove)
        {
            _fabric.Items.Remove<Account>(Items, itemToRemove);
        }

        public Account Update(Guid itemId, Account updatedItem)
        {
            return _fabric.Items.Update<Account>(Items, UpdateFunction, _folder, itemId, updatedItem);
        }
    }
}