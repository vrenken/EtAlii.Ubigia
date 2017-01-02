namespace EtAlii.Servus.PowerShell.Spaces
{
    using EtAlii.Servus.Client.Model;
    using EtAlii.Servus.PowerShell.Accounts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SpaceResolver
    {
        private readonly AddressFactory _addressFactory;
        private readonly Infrastructure _infrastructure;
        private readonly AccountResolver _accountResolver;

        public SpaceResolver(Infrastructure infrastructure, AddressFactory addressFactory, AccountResolver accountResolver)
        {
            _infrastructure = infrastructure;
            _addressFactory = addressFactory;
            _accountResolver = accountResolver;
        }

        public Space Get(ISpaceInfoProvider spaceInfoProvider)
        {
            Space space = null;

            if (spaceInfoProvider != null)
            {
                string address = null;
                var targetStorage = spaceInfoProvider.TargetStorage;

                if (spaceInfoProvider.Space != null)
                {
                    address = _addressFactory.Create(targetStorage, "management/space", "id", spaceInfoProvider.Space.Id.ToString());
                    space = !String.IsNullOrWhiteSpace(address) ? _infrastructure.Get<Space>(address) : null;
                }
                else if (!String.IsNullOrWhiteSpace(spaceInfoProvider.SpaceName))
                {
                    var targetAccount = _accountResolver.Get((IAccountInfoProvider)spaceInfoProvider);
                    address = _addressFactory.Create(targetStorage, "management/space", "accountid", targetAccount.Id.ToString());
                    var spaces = _infrastructure.Get<IEnumerable<Space>>(address);
                    space = spaces.FirstOrDefault(s => s.Name == spaceInfoProvider.SpaceName);
                }
                else if (spaceInfoProvider.SpaceId != Guid.Empty)
                {
                    address = _addressFactory.Create(targetStorage, "management/space", "id", spaceInfoProvider.SpaceId.ToString());
                    space = !String.IsNullOrWhiteSpace(address) ? _infrastructure.Get<Space>(address) : null;
                }
            }
            return space ?? SpaceCmdlet.Current;
        }
    }
}
