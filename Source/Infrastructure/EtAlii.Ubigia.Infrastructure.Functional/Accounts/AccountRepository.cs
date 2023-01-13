// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

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

    /// <inheritdoc />
    public IAsyncEnumerable<Account> GetAll()
    {
        return _logicalContext.Accounts.GetAll();
    }

    /// <inheritdoc />
    public Task<Account> Get(string accountName)
    {
        return _logicalContext.Accounts.Get(accountName);
    }

    /// <inheritdoc />
    public Task<Account> Get(string accountName, string password)
    {
        return _logicalContext.Accounts.Get(accountName, password);
    }

    /// <inheritdoc />
    public Task<Account> Get(Guid itemId)
    {
        return _logicalContext.Accounts.Get(itemId);
    }


    /// <inheritdoc />
    public Task<Account> Update(Guid itemId, Account item)
    {
        return _logicalContext.Accounts.Update(itemId, item);
    }

    /// <inheritdoc />
    public async Task<Account> Add(Account item, AccountTemplate template)
    {
        var now = DateTime.UtcNow;
        item.Created = now;
        // Nope. we are not updating so we set the updated value to null.
        item.Updated = null;

        var (addedAccount, isAdded) = await _logicalContext.Accounts.Add(item, template).ConfigureAwait(false);
        if (isAdded)
        {
            await _accountInitializer.Initialize(addedAccount, template).ConfigureAwait(false);
        }

        return addedAccount;
    }

    /// <inheritdoc />
    public Task Remove(Guid itemId)
    {
        return _logicalContext.Accounts.Remove(itemId);
    }

    /// <inheritdoc />
    public Task Remove(Account item)
    {
        return _logicalContext.Accounts.Remove(item);
    }
}
