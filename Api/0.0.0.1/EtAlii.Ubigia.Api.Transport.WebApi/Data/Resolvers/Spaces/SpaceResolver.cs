namespace EtAlii.Ubigia.Api.Management
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.WebApi;

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

        public async Task<Space> Get(ISpaceInfoProvider spaceInfoProvider, Space currentSpace, Account currentAccount)
        {
            Space space = null;

            if (spaceInfoProvider != null)
            {
                string address;
                var targetStorage = spaceInfoProvider.TargetStorage;

                if (spaceInfoProvider.Space != null)
                {
                    address = _addressFactory.Create(targetStorage, RelativeUri.Data.Spaces, UriParameter.SpaceId, spaceInfoProvider.Space.Id.ToString());
                    space = !String.IsNullOrWhiteSpace(address) ? await _client.Get<Space>(address) : null;
                }
                else if (!String.IsNullOrWhiteSpace(spaceInfoProvider.SpaceName))
                {
                    var targetAccount = await _accountResolver.Get((IAccountInfoProvider)spaceInfoProvider, currentAccount);
                    address = _addressFactory.Create(targetStorage, RelativeUri.Data.Spaces, UriParameter.AccountId, targetAccount.Id.ToString());
                    var spaces = await _client.Get<IEnumerable<Space>>(address);
                    space = spaces.FirstOrDefault(s => s.Name == spaceInfoProvider.SpaceName);
                }
                else if (spaceInfoProvider.SpaceId != Guid.Empty)
                {
                    address = _addressFactory.Create(targetStorage, RelativeUri.Data.Spaces, UriParameter.SpaceId, spaceInfoProvider.SpaceId.ToString());
                    space = !String.IsNullOrWhiteSpace(address) ? await _client.Get<Space>(address) : null;
                }
            }

            return space ?? currentSpace;
            //return space ?? SpaceCmdlet.Current;
        }
    }
}
