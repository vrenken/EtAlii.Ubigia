namespace EtAlii.Servus.PowerShell.Roots
{
    using EtAlii.Servus.Client.Model;
    using EtAlii.Servus.PowerShell.Spaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RootResolver
    {
        private readonly AddressFactory _addressFactory;
        private readonly Infrastructure _infrastructure;
        private readonly SpaceResolver _spaceResolver;

        public RootResolver(Infrastructure infrastructure, AddressFactory addressFactory, SpaceResolver spaceResolver)
        {
            _infrastructure = infrastructure;
            _addressFactory = addressFactory;
            _spaceResolver = spaceResolver;
        }

        public Root Get(IRootInfoProvider rootInfoProvider)
        {
            Root root = null;

            if (rootInfoProvider != null)
            {
                string address = null;
                var targetStorage = rootInfoProvider.TargetStorage;

                if (rootInfoProvider.Root != null)
                {
                    address = _addressFactory.Create(targetStorage, "management/root", "id", rootInfoProvider.Root.Id.ToString());
                    root = !String.IsNullOrWhiteSpace(address) ? _infrastructure.Get<Root>(address) : null;
                }
                else if (!String.IsNullOrWhiteSpace(rootInfoProvider.RootName))
                {
                    var targetSpace = _spaceResolver.Get((ISpaceInfoProvider)rootInfoProvider);
                    address = _addressFactory.Create(targetStorage, "management/root", "spaceid", targetSpace.Id.ToString());
                    var roots = _infrastructure.Get<IEnumerable<Root>>(address);
                    root = roots.FirstOrDefault(r => r.Name == rootInfoProvider.RootName);
                }
                else if (rootInfoProvider.RootId != Guid.Empty)
                {
                    address = _addressFactory.Create(targetStorage, "management/root", "id", rootInfoProvider.RootId.ToString());
                    root = !String.IsNullOrWhiteSpace(address) ? _infrastructure.Get<Root>(address) : null;
                }
            }
            return root ?? RootCmdlet.Current;
        }
    }
}
