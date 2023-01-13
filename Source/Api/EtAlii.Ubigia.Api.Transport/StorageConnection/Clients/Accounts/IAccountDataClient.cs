// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAccountDataClient : IStorageTransportClient
{
    Task<Account> Add(string accountName, string accountPassword, AccountTemplate template);
    Task Remove(Guid accountId);
    Task<Account> Change(Guid accountId, string accountName, string accountPassword);
    Task<Account> Change(Account account);
    Task<Account> Get(string accountName);
    Task<Account> Get(Guid accountId);
    IAsyncEnumerable<Account> GetAll();
}

public interface IAccountDataClient<in TTransport> : IAccountDataClient, IStorageTransportClient<TTransport>
    where TTransport: IStorageTransport
{
}
