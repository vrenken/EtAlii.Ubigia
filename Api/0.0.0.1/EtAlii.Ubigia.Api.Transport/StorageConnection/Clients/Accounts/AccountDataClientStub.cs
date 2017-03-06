namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class AccountDataClientStub : IAccountDataClient 
    {
        public async Task<Account> Add(string name, string password, AccountTemplate template)
        {
            return await Task.FromResult<Account>(null);
        }

        public async Task Remove(Guid id)
        {
            await Task.Run(() => { });
        }

        public async Task<Account> Change(Guid rootId, string accountName, string password)
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

        public async Task Connect(IStorageConnection storageConnection)
        {
            await Task.Run(() => { });
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Task.Run(() => { });
        }
    }
}
