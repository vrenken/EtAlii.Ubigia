// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public sealed class AccountDataClientStub : IAccountDataClient
{
    public Task<Account> Add(string accountName, string accountPassword, AccountTemplate template)
    {
        return Task.FromResult<Account>(null);
    }

    public Task Remove(Guid accountId)
    {
        return Task.CompletedTask;
    }

    public Task<Account> Change(Guid accountId, string accountName, string accountPassword)
    {
        return Task.FromResult<Account>(null);
    }

    public Task<Account> Change(Account account)
    {
        return Task.FromResult<Account>(null);
    }

    public Task<Account> Get(string accountName)
    {
        return Task.FromResult<Account>(null);
    }

    public Task<Account> Get(Guid accountId)
    {
        return Task.FromResult<Account>(null);
    }

    public IAsyncEnumerable<Account> GetAll()
    {
        return AsyncEnumerable.Empty<Account>();
    }

    public Task Connect(IStorageConnection storageConnection)
    {
        return Task.CompletedTask;
    }

    public Task Disconnect(IStorageConnection storageConnection)
    {
        return Task.CompletedTask;
    }
}
