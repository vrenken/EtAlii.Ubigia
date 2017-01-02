namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Api;
    using System;

    public class EntryResolver
    {
        private readonly AddressFactory _addressFactory;
        private readonly IInfrastructure _infrastructure;
        private readonly SpaceResolver _spaceResolver;

        public EntryResolver(IInfrastructure infrastructure, AddressFactory addressFactory, SpaceResolver spaceResolver)
        {
            _infrastructure = infrastructure;
            _addressFactory = addressFactory;
            _spaceResolver = spaceResolver;
        }

        public Entry Get(IEntryInfoProvider entryInfoProvider, Entry currentEntry)
        {
            Entry entry = null;

            if (entryInfoProvider != null)
            {
                string address = null;
                var targetStorage = entryInfoProvider.TargetStorage;

                if (entryInfoProvider.EntryId != null)
                {
                    address = _addressFactory.Create(targetStorage, RelativeUri.Entries, UriParameter.EntryId, entryInfoProvider.EntryId.ToString());
                    entry = !String.IsNullOrWhiteSpace(address) ? _infrastructure.Get<Entry>(address) : null;
                }
            }

            return entry ?? currentEntry;
            //return entry ?? EntryCmdlet.Current;
        }
    }
}
