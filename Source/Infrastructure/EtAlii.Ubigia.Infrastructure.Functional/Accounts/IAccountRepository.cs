// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAccountRepository
{
    Task<Account> Get(string accountName);
    Task<Account> Get(string accountName, string password);
    Task<Account> Get(Guid itemId);

    IAsyncEnumerable<Account> GetAll();

    Task<Account> Add(Account item, AccountTemplate template);

    Task Remove(Guid itemId);
    Task Remove(Account item);

    Task<Account> Update(Guid itemId, Account item);
}
