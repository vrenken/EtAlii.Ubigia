namespace EtAlii.Ubigia.PowerShell.Roots
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.PowerShell.Spaces;
    using EtAlii.Ubigia.PowerShell.Storages;

    /// <summary>
    /// A resolver able to retrieve roots.
    /// </summary>
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

        /// <summary>
        /// Get a root using the specified info provider, current account, space and root.
        /// </summary>
        /// <param name="rootInfoProvider"></param>
        /// <param name="currentAccount"></param>
        /// <param name="currentSpace"></param>
        /// <param name="currentRoot"></param>
        /// <returns></returns>
        public async Task<Root> Get(IRootInfoProvider rootInfoProvider, Account currentAccount, Space currentSpace, Root currentRoot)
        {
            Root root = null;

            if (rootInfoProvider != null)
            {
                Uri address;

                if (rootInfoProvider.Root != null)
                {
                    address = _addressFactory.Create(StorageCmdlet.CurrentDataApiAddress, RelativeDataUri.Roots, UriParameter.RootId, rootInfoProvider.Root.Id.ToString());
                    root = address != null ? await _client.Get<Root>(address).ConfigureAwait(false) : null;
                }
                else if (rootInfoProvider.RootId != Guid.Empty)
                {
                    var targetSpace = await _spaceResolver.Get((ISpaceInfoProvider)rootInfoProvider, currentSpace, currentAccount).ConfigureAwait(false);
                    address = _addressFactory.Create(StorageCmdlet.CurrentDataApiAddress, RelativeDataUri.Roots, UriParameter.SpaceId, targetSpace.Id.ToString(), UriParameter.RootId, rootInfoProvider.RootId.ToString(), UriParameter.IdType, "rootId");
                    root = address != null ? await _client.Get<Root>(address).ConfigureAwait(false) : null;
                }
                else if (!string.IsNullOrEmpty(rootInfoProvider.RootName))
                {
                    var targetSpace = await _spaceResolver.Get((ISpaceInfoProvider)rootInfoProvider, currentSpace, currentAccount).ConfigureAwait(false);
                    address = _addressFactory.Create(StorageCmdlet.CurrentDataApiAddress, RelativeDataUri.Roots, UriParameter.SpaceId, targetSpace.Id.ToString(), UriParameter.RootId, rootInfoProvider.RootName, UriParameter.IdType, "rootName");
                    root = address != null ? await _client.Get<Root>(address).ConfigureAwait(false) : null;
                }
            }
            return root ?? currentRoot;
        }
    }
}
