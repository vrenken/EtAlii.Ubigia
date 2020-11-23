namespace EtAlii.Ubigia.PowerShell.Accounts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.PowerShell.Storages;

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
                Uri address;
                if (accountInfoProvider.Account != null)
                {
                    address = _addressFactory.Create(StorageCmdlet.CurrentManagementApiAddress, RelativeUri.Data.Accounts, UriParameter.AccountId, accountInfoProvider.Account.Id.ToString());
                    account = address != null ? await _client.Get<Account>(address).ConfigureAwait(false) : null;
                }
                else if (!string.IsNullOrWhiteSpace(accountInfoProvider.AccountName))
                {
                    address = _addressFactory.Create(StorageCmdlet.CurrentManagementApiAddress, RelativeUri.Data.Accounts);
                    var accounts = await _client.Get<IEnumerable<Account>>(address).ConfigureAwait(false);
                    account = accounts.FirstOrDefault(u => u.Name == accountInfoProvider.AccountName);
                }
                else if (accountInfoProvider.AccountId != Guid.Empty)
                {
                    address = _addressFactory.Create(StorageCmdlet.CurrentManagementApiAddress, RelativeUri.Data.Accounts, UriParameter.AccountId, accountInfoProvider.AccountId.ToString());
                    account = address != null ? await _client.Get<Account>(address).ConfigureAwait(false) : null;
                }
            }

            return account ?? currentAccount;
        }
    }
}
