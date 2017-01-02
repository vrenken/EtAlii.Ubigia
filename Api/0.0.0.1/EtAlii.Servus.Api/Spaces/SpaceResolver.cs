namespace EtAlii.Servus.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SpaceResolver
    {
        private readonly IAddressFactory _addressFactory;
        private readonly IInfrastructureClient _client;
        private readonly AccountResolver _accountResolver;

        public SpaceResolver(IInfrastructureClient client, IAddressFactory addressFactory, AccountResolver accountResolver)
        {
            _client = client;
            _addressFactory = addressFactory;
            _accountResolver = accountResolver;
        }

        public Space Get(ISpaceInfoProvider spaceInfoProvider, Space currentSpace, Account currentAccount)
        {
            Space space = null;

            if (spaceInfoProvider != null)
            {
                string address;
                var targetStorage = spaceInfoProvider.TargetStorage;

                if (spaceInfoProvider.Space != null)
                {
                    address = _addressFactory.Create(targetStorage, RelativeUri.Spaces, UriParameter.SpaceId, spaceInfoProvider.Space.Id.ToString());
                    space = !String.IsNullOrWhiteSpace(address) ? _client.Get<Space>(address) : null;
                }
                else if (!String.IsNullOrWhiteSpace(spaceInfoProvider.SpaceName))
                {
                    var targetAccount = _accountResolver.Get((IAccountInfoProvider)spaceInfoProvider, currentAccount);
                    address = _addressFactory.Create(targetStorage, RelativeUri.Spaces, UriParameter.AccountId, targetAccount.Id.ToString());
                    var spaces = _client.Get<IEnumerable<Space>>(address);
                    space = spaces.FirstOrDefault(s => s.Name == spaceInfoProvider.SpaceName);
                }
                else if (spaceInfoProvider.SpaceId != Guid.Empty)
                {
                    address = _addressFactory.Create(targetStorage, RelativeUri.Spaces, UriParameter.SpaceId, spaceInfoProvider.SpaceId.ToString());
                    space = !String.IsNullOrWhiteSpace(address) ? _client.Get<Space>(address) : null;
                }
            }

            return space ?? currentSpace;
            //return space ?? SpaceCmdlet.Current;
        }
    }
}
