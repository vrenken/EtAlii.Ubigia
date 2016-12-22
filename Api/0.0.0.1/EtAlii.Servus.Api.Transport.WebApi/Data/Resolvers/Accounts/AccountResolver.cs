namespace EtAlii.Servus.Api.Management
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.WebApi;

    public class AccountResolver : IAccountResolver
    {
        private readonly IAddressFactory _addressFactory;
        private readonly IInfrastructureClient _client;

        public AccountResolver(IInfrastructureClient client, IAddressFactory addressFactory)
        {
            _client = client;
            _addressFactory = addressFactory;
        }

        public async Task<Account> Get(IAccountInfoProvider accountInfoProvider, Account currentAccount, Storage currentStorage = null)
        {
            Account account = null;

            if (accountInfoProvider != null)
            {
                string address;
                var targetStorage = currentStorage ?? accountInfoProvider.TargetStorage;

                if (accountInfoProvider.Account != null)
                {
                    address = _addressFactory.Create(targetStorage, RelativeUri.Data.Accounts, UriParameter.AccountId, accountInfoProvider.Account.Id.ToString());
                    account = !String.IsNullOrWhiteSpace(address) ? await _client.Get<Account>(address) : null;
                }
                else if (!String.IsNullOrWhiteSpace(accountInfoProvider.AccountName))
                {
                    address = _addressFactory.Create(targetStorage, RelativeUri.Data.Accounts);
                    var accounts = await _client.Get<IEnumerable<Account>>(address);
                    account = accounts.FirstOrDefault(u => u.Name == accountInfoProvider.AccountName);
                }
                else if (accountInfoProvider.AccountId != Guid.Empty)
                {
                    address = _addressFactory.Create(targetStorage, RelativeUri.Data.Accounts, UriParameter.AccountId, accountInfoProvider.AccountId.ToString());
                    account = !String.IsNullOrWhiteSpace(address) ? await _client.Get<Account>(address) : null;
                }
            }

            return account ?? currentAccount;
        }
    }
}
