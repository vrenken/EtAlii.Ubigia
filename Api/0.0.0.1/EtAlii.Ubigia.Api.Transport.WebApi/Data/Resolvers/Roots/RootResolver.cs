namespace EtAlii.Ubigia.Api.Management
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    public class RootResolver : IRootResolver
    {
        private readonly IAddressFactory _addressFactory;
        private readonly IInfrastructureClient _client;
        private readonly ISpaceResolver _spaceResolver;

        public RootResolver(IInfrastructureClient client, IAddressFactory addressFactory, ISpaceResolver spaceResolver)
        {
            _client = client;
            _addressFactory = addressFactory;
            _spaceResolver = spaceResolver;
        }

        public async Task<Root> Get(IRootInfoProvider rootInfoProvider, Account currentAccount, Space currentSpace, Root currentRoot)
        {
            Root root = null;

            if (rootInfoProvider != null)
            {
                string address;
                var targetStorage = rootInfoProvider.TargetStorage;

                if (rootInfoProvider.Root != null)
                {
                    address = _addressFactory.Create(targetStorage, RelativeUri.Data.Roots, UriParameter.RootId, rootInfoProvider.Root.Id.ToString());
                    root = !String.IsNullOrWhiteSpace(address) ? await _client.Get<Root>(address) : null;
                }
                else if (rootInfoProvider.RootId != Guid.Empty)
                {
                    var targetSpace = await _spaceResolver.Get((ISpaceInfoProvider)rootInfoProvider, currentSpace, currentAccount);
                    address = _addressFactory.Create(targetStorage, RelativeUri.Data.Roots, UriParameter.SpaceId, targetSpace.Id.ToString(), UriParameter.RootId, rootInfoProvider.RootId.ToString());
                    root = !String.IsNullOrWhiteSpace(address) ? await _client.Get<Root>(address) : null;
                }
                else if (!String.IsNullOrEmpty(rootInfoProvider.RootName))
                {
                    var targetSpace = await _spaceResolver.Get((ISpaceInfoProvider)rootInfoProvider, currentSpace, currentAccount);
                    address = _addressFactory.Create(targetStorage, RelativeUri.Data.Roots, UriParameter.SpaceId, targetSpace.Id.ToString(), UriParameter.RootName, rootInfoProvider.RootName);
                    root = !String.IsNullOrWhiteSpace(address) ? await _client.Get<Root>(address) : null;
                }
            }
            return root ?? currentRoot;
        }
    }
}
