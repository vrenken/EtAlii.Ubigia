namespace EtAlii.Servus.Api
{
    using System;

    public class RootResolver
    {
        private readonly IAddressFactory _addressFactory;
        private readonly IInfrastructureClient _client;
        private readonly SpaceResolver _spaceResolver;

        public RootResolver(IInfrastructureClient client, IAddressFactory addressFactory, SpaceResolver spaceResolver)
        {
            _client = client;
            _addressFactory = addressFactory;
            _spaceResolver = spaceResolver;
        }

        public Root Get(IRootInfoProvider rootInfoProvider, Account currentAccount, Space currentSpace, Root currentRoot)
        {
            Root root = null;

            if (rootInfoProvider != null)
            {
                string address;
                var targetStorage = rootInfoProvider.TargetStorage;

                if (rootInfoProvider.Root != null)
                {
                    address = _addressFactory.Create(targetStorage, RelativeUri.Roots, UriParameter.RootId, rootInfoProvider.Root.Id.ToString());
                    root = !String.IsNullOrWhiteSpace(address) ? _client.Get<Root>(address) : null;
                }
                else if (rootInfoProvider.RootId != Guid.Empty)
                {
                    var targetSpace = _spaceResolver.Get((ISpaceInfoProvider)rootInfoProvider, currentSpace, currentAccount);
                    address = _addressFactory.Create(targetStorage, RelativeUri.Roots, UriParameter.SpaceId, targetSpace.Id.ToString(), UriParameter.RootId, rootInfoProvider.RootId.ToString());
                    root = !String.IsNullOrWhiteSpace(address) ? _client.Get<Root>(address) : null;
                }
                else if (!String.IsNullOrEmpty(rootInfoProvider.RootName))
                {
                    var targetSpace = _spaceResolver.Get((ISpaceInfoProvider)rootInfoProvider, currentSpace, currentAccount);
                    address = _addressFactory.Create(targetStorage, RelativeUri.Roots, UriParameter.SpaceId, targetSpace.Id.ToString(), UriParameter.RootName, rootInfoProvider.RootName);
                    root = !String.IsNullOrWhiteSpace(address) ? _client.Get<Root>(address) : null;
                }
            }
            return root ?? currentRoot;
        }
    }
}
