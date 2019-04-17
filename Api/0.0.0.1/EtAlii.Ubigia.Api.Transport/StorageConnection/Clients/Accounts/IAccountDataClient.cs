﻿namespace EtAlii.Ubigia.Api.Transport
{
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
        Task<IEnumerable<Account>> GetAll();
    }

    public interface IAccountDataClient<in TTransport> : IAccountDataClient, IStorageTransportClient<TTransport>
        where TTransport: IStorageTransport
    {
    }
}
