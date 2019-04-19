namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;

    public interface IAccountRepository 
    {
		Account Get(string accountName);
		Account Get(string accountName, string password);

        IEnumerable<Account> GetAll();
        Account Get(Guid itemId);

        Task<Account> Add(Account item, AccountTemplate template);

        void Remove(Guid itemId);
        void Remove(Account item);

        Account Update(Guid itemId, Account item);
    }
}