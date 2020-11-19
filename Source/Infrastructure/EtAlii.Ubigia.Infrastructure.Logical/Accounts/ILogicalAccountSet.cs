namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    public interface ILogicalAccountSet
    {
        Account Get(string accountName);
        Account Get(string accountName, string password);

        Account Add(Account item, AccountTemplate template, out bool isAdded);

        IAsyncEnumerable<Account> GetAll();

        Account Get(Guid id);

        Task<ObservableCollection<Account>> GetItems();

        void Remove(Guid itemId);

        void Remove(Account itemToRemove);

        Account Update(Guid itemId, Account updatedItem);
    }
}