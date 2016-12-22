namespace EtAlii.Servus.Api.Management
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.WebApi;

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
                    var address = _addressFactory.Create(targetStorage, RelativeUri.Data.Entry, UriParameter.EntryId, entryInfoProvider.EntryId.ToString());
                    entry = !String.IsNullOrWhiteSpace(address) ? await _client.Get<Entry>(address) : null;
                }
            }

            return entry ?? currentEntry;
            //return entry ?? EntryCmdlet.Current;
        }
    }
}
