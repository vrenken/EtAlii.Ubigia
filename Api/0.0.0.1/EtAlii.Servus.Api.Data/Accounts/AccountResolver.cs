namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AccountResolver
    {
        private readonly AddressFactory _addressFactory;
        private readonly Infrastructure _infrastructure;

        private const string _relativePath = "management/account";

        public AccountResolver(Infrastructure infrastructure, AddressFactory addressFactory)
        {
            _infrastructure = infrastructure;
            _addressFactory = addressFactory;
        }

        public Account Get(IAccountInfoProvider accountInfoProvider, Account currentAccount, Storage currentStorage = null)
        {
            Account account = null;

            if (accountInfoProvider != null)
            {
                string address = null;
                var targetStorage = currentStorage ?? accountInfoProvider.TargetStorage;

                if (accountInfoProvider.Account != null)
                {
                    address = _addressFactory.Create(targetStorage, _relativePath, UriParameter.AccountId, accountInfoProvider.Account.Id.ToString());
                    account = !String.IsNullOrWhiteSpace(address) ? _infrastructure.Get<Account>(address) : null;
                }
                else if (!String.IsNullOrWhiteSpace(accountInfoProvider.AccountName))
                {
                    address = _addressFactory.Create(targetStorage, _relativePath);
                    var accounts = _infrastructure.Get<IEnumerable<Account>>(address);
                    account = accounts.FirstOrDefault(u => u.Name == accountInfoProvider.AccountName);
                }
                else if (accountInfoProvider.AccountId != Guid.Empty)
                {
                    address = _addressFactory.Create(targetStorage, _relativePath, UriParameter.AccountId, accountInfoProvider.AccountId.ToString());
                    account = !String.IsNullOrWhiteSpace(address) ? _infrastructure.Get<Account>(address) : null;
                }
            }

            return account ?? currentAccount;
        }
    }
}
