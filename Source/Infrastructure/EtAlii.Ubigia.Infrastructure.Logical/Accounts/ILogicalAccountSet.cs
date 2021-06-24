// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;

    public interface ILogicalAccountSet
    {
        IAsyncEnumerable<Account> GetAll();
        Account Get(Guid id);
        Account Get(string accountName);
        Account Get(string accountName, string password);

        Account Add(Account item, AccountTemplate template, out bool isAdded);

        void Remove(Guid itemId);

        void Remove(Account itemToRemove);

        Account Update(Guid itemId, Account updatedItem);
    }
}