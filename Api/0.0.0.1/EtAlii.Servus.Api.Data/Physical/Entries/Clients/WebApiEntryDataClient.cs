namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using EtAlii.xTechnology.MicroContainer;
    using System.Collections.Generic;

    public class WebApiEntryDataClient : WebApiDataClientBase<IDataConnection>, IEntryDataClient 
    {
        public WebApiEntryDataClient(Container container, IAddressFactory addressFactory, IInfrastructureClient client)
            : base(container, addressFactory, client)
        {
        }

        public IEditableEntry Prepare()
        {
            var address = AddressFactory.Create(Connection.Storage, RelativeUri.Entry, UriParameter.SpaceId, Connection.Space.Id.ToString());
            var entry = Client.Post<Entry>(address);

            return entry;
        }

        public IReadOnlyEntry Change(IEditableEntry entry)
        {
            var changeAddress = AddressFactory.Create(Connection.Storage, RelativeUri.Entry);
            entry = Client.Put(changeAddress, entry as Entry);
            return entry as IReadOnlyEntry;
        }

        public IReadOnlyEntry Get(Root root, EntryRelation entryRelations = EntryRelation.None)
        {
            return Get(root.Identifier, entryRelations);
        }

        public IReadOnlyEntry Get(Identifier entryIdentifier, EntryRelation entryRelations = EntryRelation.None)
        {
            var address = AddressFactory.Create(Connection.Storage, RelativeUri.Entry, UriParameter.EntryId, entryIdentifier.ToString(), UriParameter.EntryRelations, entryRelations.ToString());
            var entry = Client.Get<Entry>(address);
            return entry;
        }

        public IEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> entryIdentifiers, EntryRelation entryRelations = EntryRelation.None)
        {
            var result = new List<IReadOnlyEntry>();
            foreach (var entryIdentifier in entryIdentifiers)
            {
                var address = AddressFactory.Create(Connection.Storage, RelativeUri.Entries, UriParameter.EntryId,
                    entryIdentifier.ToString(), UriParameter.EntryRelations, entryRelations.ToString());
                var entry = Client.Get<Entry>(address);
                result.Add(entry);
            }
            return result;
        }

        public IEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation, EntryRelation entryRelations = EntryRelation.None)
        {
            var address = AddressFactory.Create(Connection.Storage, RelativeUri.RelatedEntries, UriParameter.EntryId, entryIdentifier.ToString(), UriParameter.EntriesWithRelation, entriesWithRelation.ToString(), UriParameter.EntryRelations, entryRelations.ToString());
            var entries = Client.Get<IEnumerable<Entry>>(address);
            return entries;
        }
    }
}
