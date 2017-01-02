namespace EtAlii.Servus.Api
{
    using System;

    public class EntryResolver
    {
        private readonly IAddressFactory _addressFactory;
        private readonly IInfrastructureClient _client;
        private readonly SpaceResolver _spaceResolver;

        public EntryResolver(IInfrastructureClient client, IAddressFactory addressFactory, SpaceResolver spaceResolver)
        {
            _client = client;
            _addressFactory = addressFactory;
            _spaceResolver = spaceResolver;
        }

        public Entry Get(IEntryInfoProvider entryInfoProvider, Entry currentEntry)
        {
            Entry entry = null;

            if (entryInfoProvider != null)
            {
                var targetStorage = entryInfoProvider.TargetStorage;

                if (entryInfoProvider.EntryId != Identifier.Empty)
                {
                    var address = _addressFactory.Create(targetStorage, RelativeUri.Entries, UriParameter.EntryId, entryInfoProvider.EntryId.ToString());
                    entry = !String.IsNullOrWhiteSpace(address) ? _client.Get<Entry>(address) : null;
                }
            }

            return entry ?? currentEntry;
            //return entry ?? EntryCmdlet.Current;
        }
    }
}
