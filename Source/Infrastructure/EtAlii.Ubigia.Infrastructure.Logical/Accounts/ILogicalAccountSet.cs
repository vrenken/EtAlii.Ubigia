﻿namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public interface ILogicalAccountSet
    {
        Account Get(string accountName);
        Account Get(string accountName, string password);

        Account Add(Account item, AccountTemplate template, out bool isAdded);

        IEnumerable<Account> GetAll();

        Account Get(Guid id);

        ObservableCollection<Account> GetItems();

        void Remove(Guid itemId);

        void Remove(Account itemToRemove);

        Account Update(Guid itemId, Account updatedItem);
    }
}