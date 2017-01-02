namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Collections.Generic;

    public class Accounts : CollectionBase<StorageConnection>
    {
        internal Accounts(StorageConnection connection)
            : base(connection)
        {
        }

        internal const string RelativePath = "management/account";

        public Account Add(string accountName, string password)
        {
            if (Connection.CurrentStorage == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            var address = AddressFactory.Create(Connection.CurrentStorage, RelativePath);
            var account = new Account
            {
                Name = accountName,
                Password = password,
            };

            account = Infrastructure.Post<Account>(address, account);
            return account;
        }

        public void Remove(Guid accountId)
        {
            if (Connection.CurrentStorage == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            var address = AddressFactory.Create(Connection.CurrentStorage, RelativePath, UriParameter.AccountId, accountId.ToString());
            Infrastructure.Delete(address);
        }

        public Account Change(Guid accountId, string accountName, string accountPassword)
        {
            if (Connection.CurrentStorage == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            var account = new Account
            {
                Id = accountId,
                Name = accountName,
                Password = accountPassword,
            };

            var changeAddress = AddressFactory.Create(Connection.CurrentStorage, RelativePath, UriParameter.AccountId, accountId.ToString());
            account = Infrastructure.Put(changeAddress, account);
            return account;
        }

        public Account Get(string accountName)
        {
            if (Connection.CurrentStorage == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            var address = AddressFactory.Create(Connection.CurrentStorage, RelativePath, UriParameter.AccountName, accountName);
            var account = Infrastructure.Get<Account>(address);
            return account;
        }

        public Account Get(Guid accountId)
        {
            if (Connection.CurrentStorage == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            var address = AddressFactory.Create(Connection.CurrentStorage, RelativePath, UriParameter.AccountId, accountId.ToString());
            var account = Infrastructure.Get<Account>(address);
            return account;
        }

        public IEnumerable<Account> GetAll()
        {
            if (Connection.CurrentStorage == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            var address = AddressFactory.Create(Connection.CurrentStorage, RelativePath);
            var accounts = Infrastructure.Get<IEnumerable<Account>>(address);
            return accounts;
        }
    }
}
