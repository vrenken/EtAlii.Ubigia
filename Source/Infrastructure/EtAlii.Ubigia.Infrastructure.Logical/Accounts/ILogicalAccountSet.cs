// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILogicalAccountSet
    {
        IAsyncEnumerable<Account> GetAll();
        Task<Account> Get(Guid id);
        Task<Account> Get(string accountName);
        Task<Account> Get(string accountName, string password);

        Task<(Account, bool)> Add(Account item, AccountTemplate template);

        Task Remove(Guid itemId);

        Task Remove(Account itemToRemove);

        Task<Account> Update(Guid itemId, Account updatedItem);
    }
}
