namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RootResolver
    {
        private readonly AddressFactory _addressFactory;
        private readonly Infrastructure _infrastructure;
        private readonly SpaceResolver _spaceResolver;

        private const string _relativePath = "management/root";

        public RootResolver(Infrastructure infrastructure, AddressFactory addressFactory, SpaceResolver spaceResolver)
        {
            _infrastructure = infrastructure;
            _addressFactory = addressFactory;
            _spaceResolver = spaceResolver;
        }

        public Root Get(IRootInfoProvider rootInfoProvider, Account currentAccount, Space currentSpace, Root currentRoot)
        {
            Root root = null;

            if (rootInfoProvider != null)
            {
                string address = null;
                var targetStorage = rootInfoProvider.TargetStorage;

                if (rootInfoProvider.Root != null)
                {
                    address = _addressFactory.Create(targetStorage, _relativePath, UriParameter.RootId, rootInfoProvider.Root.Id.ToString());
                    root = !String.IsNullOrWhiteSpace(address) ? _infrastructure.Get<Root>(address) : null;
                }
                else if (rootInfoProvider.RootId != Guid.Empty)
                {
                    var targetSpace = _spaceResolver.Get((ISpaceInfoProvider)rootInfoProvider, currentSpace, currentAccount);
                    address = _addressFactory.Create(targetStorage, _relativePath, UriParameter.SpaceId, targetSpace.Id.ToString(), UriParameter.RootId, rootInfoProvider.RootId.ToString());
                    root = !String.IsNullOrWhiteSpace(address) ? _infrastructure.Get<Root>(address) : null;
                }
                else if (!String.IsNullOrEmpty(rootInfoProvider.RootName))
                {
                    var targetSpace = _spaceResolver.Get((ISpaceInfoProvider)rootInfoProvider, currentSpace, currentAccount);
                    address = _addressFactory.Create(targetStorage, _relativePath, UriParameter.SpaceId, targetSpace.Id.ToString(), UriParameter.RootName, rootInfoProvider.RootName.ToString());
                    root = !String.IsNullOrWhiteSpace(address) ? _infrastructure.Get<Root>(address) : null;
                }
            }
            return root ?? currentRoot;
        }
    }
}
