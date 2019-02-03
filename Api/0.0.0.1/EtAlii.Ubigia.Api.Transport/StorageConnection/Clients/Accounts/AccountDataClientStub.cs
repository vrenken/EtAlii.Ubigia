namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class AccountDataClientStub : IAccountDataClient 
    {
        public async Task<Account> Add(string accountName, string accountPassword, AccountTemplate template)
        {
            return await Task.FromResult<Account>(null);
        }

        public async Task Remove(Guid accountId)
        {
            await Task.CompletedTask;
        }

        public async Task<Account> Change(Guid accountId, string accountName, string accountPassword)
        {
            return await Task.FromResult<Account>(null);
        }

        public async Task<Account> Change(Account account)
        {
            return await Task.FromResult<Account>(null);
        }

        public async Task<Account> Get(string accountName)
        {
            return await Task.FromResult<Account>(null);
        }

        public async Task<Account> Get(Guid rootId)
        {
            return await Task.FromResult<Account>(null);
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await Task.FromResult<IEnumerable<Account>>(null);
        }

        public async Task Connect(IStorageConnection connection)
        {
            await Task.CompletedTask;
        }

        public async Task Disconnect(IStorageConnection connection)
        {
            await Task.CompletedTask;
        }
    }
}
