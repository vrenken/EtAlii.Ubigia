﻿namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System;
    using System.Threading.Tasks;

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
                Uri address;
                var targetStorage = rootInfoProvider.TargetStorage;

                if (rootInfoProvider.Root != null)
                {
                    address = _addressFactory.Create(targetStorage, RelativeUri.Data.Roots, UriParameter.RootId, rootInfoProvider.Root.Id.ToString());
                    root = address != null ? await _client.Get<Root>(address) : null;
                }
                else if (rootInfoProvider.RootId != Guid.Empty)
                {
                    var targetSpace = await _spaceResolver.Get((ISpaceInfoProvider)rootInfoProvider, currentSpace, currentAccount);
                    address = _addressFactory.Create(targetStorage, RelativeUri.Data.Roots, UriParameter.SpaceId, targetSpace.Id.ToString(), UriParameter.RootId, rootInfoProvider.RootId.ToString(), UriParameter.IdType, "rootId");
                    root = address != null ? await _client.Get<Root>(address) : null;
                }
                else if (!String.IsNullOrEmpty(rootInfoProvider.RootName))
                {
                    var targetSpace = await _spaceResolver.Get((ISpaceInfoProvider)rootInfoProvider, currentSpace, currentAccount);
                    address = _addressFactory.Create(targetStorage, RelativeUri.Data.Roots, UriParameter.SpaceId, targetSpace.Id.ToString(), UriParameter.RootId, rootInfoProvider.RootName, UriParameter.IdType, "rootName");
                    root = address != null ? await _client.Get<Root>(address) : null;
                }
            }
            return root ?? currentRoot;
        }
    }
}
