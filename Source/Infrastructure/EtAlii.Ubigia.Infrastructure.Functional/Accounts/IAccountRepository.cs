namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAccountRepository 
    {
		Account Get(string accountName);
		Account Get(string accountName, string password);
        Account Get(Guid itemId);

        IAsyncEnumerable<Account> GetAll();

        Task<Account> Add(Account item, AccountTemplate template);

        void Remove(Guid itemId);
        void Remove(Account item);

        Account Update(Guid itemId, Account item);
    }
}