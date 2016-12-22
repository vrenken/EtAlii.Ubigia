namespace EtAlii.Servus.Api.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAccountContext : IStorageClientContext
    {
        Task<Account> Add(string accountName, string accountPassword, AccountTemplate accountTemplate);
        Task Remove(Guid accountId);
        Task<Account> Change(Guid accountId, string accountName, string accountPassword);
        Task<Account> Change(Account account);
        Task<Account> Get(string accountName);
        Task<Account> Get(Guid accountId);
        Task<IEnumerable<Account>> GetAll();
    }
}
