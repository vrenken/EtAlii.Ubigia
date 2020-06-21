namespace EtAlii.Ubigia.PowerShell.Entries
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.PowerShell.Storages;

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
                if (entryInfoProvider.EntryId != Identifier.Empty)
                {
                    var address = _addressFactory.Create(StorageCmdlet.CurrentStorageApiAddress, RelativeUri.Data.Entry, UriParameter.EntryId, entryInfoProvider.EntryId.ToString());
                    entry = address != null ? await _client.Get<Entry>(address) : null;
                }
            }

            return entry ?? currentEntry;
            //return entry ?? EntryCmdlet.Current
        }
    }
}
