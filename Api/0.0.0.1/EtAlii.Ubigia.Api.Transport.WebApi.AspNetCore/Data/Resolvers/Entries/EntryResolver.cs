namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System;
    using System.Threading.Tasks;

    public class EntryResolver : IEntryResolver
    {
        private readonly IAddressFactory _addressFactory;
        private readonly IInfrastructureClient _client;

        public EntryResolver(IInfrastructureClient client, IAddressFactory addressFactory)
        {
            _client = client;
            _addressFactory = addressFactory;
        }

        public async Task<Entry> Get(IEntryInfoProvider entryInfoProvider, Entry currentEntry)
        {
            Entry entry = null;

            if (entryInfoProvider != null)
            {
                var targetStorage = entryInfoProvider.TargetStorage;

                if (entryInfoProvider.EntryId != Identifier.Empty)
                {
                    var address = _addressFactory.Create(targetStorage, RelativeUri.ApiRest + RelativeUri.Data.Entry, UriParameter.EntryId, entryInfoProvider.EntryId.ToString());
                    entry = address != null ? await _client.Get<Entry>(address) : null;
                }
            }

            return entry ?? currentEntry;
            //return entry ?? EntryCmdlet.Current;
        }
    }
}
