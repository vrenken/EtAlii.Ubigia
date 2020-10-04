namespace EtAlii.Ubigia.Api.Transport.Management.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    internal class WebApiAccountDataClient : WebApiClientBase, IAccountDataClient
    {
        public async Task<Account> Add(string accountName, string accountPassword, AccountTemplate template)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeUri.Data.Accounts, UriParameter.AccountTemplate, template.Name);
            var account = new Account
            {
                Name = accountName,
                Password = accountPassword,
            };

            account = await Connection.Client.Post(address, account);
            return account;
        }

        public async Task Remove(Guid accountId)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeUri.Data.Accounts, UriParameter.AccountId, accountId.ToString());
            await Connection.Client.Delete(address);
        }

        public async Task<Account> Change(Guid accountId, string accountName, string accountPassword)
        {
            var account = new Account
            {
                Id = accountId,
                Name = accountName,
                Password = accountPassword,
            };

            var changeAddress = Connection.AddressFactory.Create(Connection.Transport, RelativeUri.Data.Accounts, UriParameter.AccountId, accountId.ToString());
            account = await Connection.Client.Put(changeAddress, account);
            return account;
        }

        public async Task<Account> Change(Account account)
        {
            var changeAddress = Connection.AddressFactory.Create(Connection.Transport, RelativeUri.Data.Accounts, UriParameter.AccountId, account.Id.ToString());
            account = await Connection.Client.Put(changeAddress, account);
            return account;
        }

        public async Task<Account> Get(string accountName)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeUri.Data.Accounts, UriParameter.AccountName, accountName);
            var account = await Connection.Client.Get<Account>(address);
            return account;
        }

        public async Task<Account> Get(Guid accountId)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeUri.Data.Accounts, UriParameter.AccountId, accountId.ToString());
            var account = await Connection.Client.Get<Account>(address);
            return account;
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeUri.Data.Accounts);
            var accounts = await Connection.Client.Get<IEnumerable<Account>>(address);
            return accounts;
        }
    }
}
