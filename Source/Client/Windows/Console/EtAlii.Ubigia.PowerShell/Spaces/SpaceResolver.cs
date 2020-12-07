namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.PowerShell.Accounts;
    using EtAlii.Ubigia.PowerShell.Storages;

    /// <summary>
    /// A resolver able to retrieve spaces.
    /// </summary>
    public class SpaceResolver : ISpaceResolver
    {
        private readonly IAddressFactory _addressFactory;
        private readonly IInfrastructureClient _client;
        private readonly IAccountResolver _accountResolver;

        public SpaceResolver(IInfrastructureClient client, IAddressFactory addressFactory, IAccountResolver accountResolver)
        {
            _client = client;
            _addressFactory = addressFactory;
            _accountResolver = accountResolver;
        }

        /// <summary>
        /// Get a space using the specified info provider, current space and account.
        /// </summary>
        /// <param name="spaceInfoProvider"></param>
        /// <param name="currentSpace"></param>
        /// <param name="currentAccount"></param>
        /// <returns></returns>
        public async Task<Space> Get(ISpaceInfoProvider spaceInfoProvider, Space currentSpace, Account currentAccount)
        {
            Space space = null;

            if (spaceInfoProvider != null)
            {
                Uri address;

                if (spaceInfoProvider.Space != null)
                {
                    address = _addressFactory.Create(StorageCmdlet.CurrentManagementApiAddress, RelativeDataUri.Spaces, UriParameter.SpaceId, spaceInfoProvider.Space.Id.ToString());
                    space = address != null ? await _client.Get<Space>(address).ConfigureAwait(false) : null;
                }
                else if (!string.IsNullOrWhiteSpace(spaceInfoProvider.SpaceName))
                {
                    var targetAccount = await _accountResolver.Get((IAccountInfoProvider)spaceInfoProvider, currentAccount).ConfigureAwait(false);
                    address = _addressFactory.Create(StorageCmdlet.CurrentManagementApiAddress, RelativeDataUri.Spaces, UriParameter.AccountId, targetAccount.Id.ToString());
                    var spaces = await _client.Get<IEnumerable<Space>>(address).ConfigureAwait(false);
                    space = spaces.FirstOrDefault(s => s.Name == spaceInfoProvider.SpaceName);
                }
                else if (spaceInfoProvider.SpaceId != Guid.Empty)
                {
                    address = _addressFactory.Create(StorageCmdlet.CurrentManagementApiAddress, RelativeDataUri.Spaces, UriParameter.SpaceId, spaceInfoProvider.SpaceId.ToString());
                    space = address != null ? await _client.Get<Space>(address).ConfigureAwait(false) : null;
                }
            }

            return space ?? currentSpace;
            //return space ?? SpaceCmdlet.Current
        }
    }
}
