namespace EtAlii.Ubigia.Infrastructure.Hosting.Local
{
    using EtAlii.Ubigia;
    using EtAlii.xTechnology.MicroContainer;
    using System.Collections.Generic;

    public class LocalEntryDataClient : RestDataClientBase<IDataConnection>, IEntryDataClient
    {
        public LocalEntryDataClient(Container container, IAddressFactory addressFactory, IInfrastructureClient client)
            : base(container, addressFactory, client)
        {
        }

        public IEditableEntry Prepare()
        {
            throw new System.NotImplementedException();

            //var address = AddressFactory.Create(Connection.Storage, RelativeUri.Entries, UriParameter.SpaceId, Connection.Space.Id.ToString())
            //var entry = Infrastructure.Post<Entry>(address)

            //return entry
        }

        public IReadOnlyEntry Change(IEditableEntry entry)
        {
            throw new System.NotImplementedException();

            //var changeAddress = AddressFactory.Create(Connection.Storage, RelativeUri.Entries)
            //entry = Infrastructure.Put(changeAddress, entry as Entry)
            //return entry as IReadOnlyEntry
        }

        public IReadOnlyEntry Get(Root root, EntryRelation entryRelations = EntryRelation.None)
        {
            throw new System.NotImplementedException();

            //return Get(root.Identifier, entryRelations)
        }

        public IReadOnlyEntry Get(in Identifier entryIdentifier, EntryRelation entryRelations = EntryRelation.None)
        {
            throw new System.NotImplementedException();

            //var address = AddressFactory.Create(Connection.Storage, RelativeUri.Entries, UriParameter.EntryId, entryIdentifier.ToString(), UriParameter.EntryRelations, entryRelations.ToString())
            //var entry = Infrastructure.Get<Entry>(address)
            //return entry
        }

        public IEnumerable<IReadOnlyEntry> GetRelated(in Identifier entryIdentifier, EntryRelation entriesWithRelation, EntryRelation entryRelations = EntryRelation.None)
        {
            throw new System.NotImplementedException();

            //var address = AddressFactory.Create(Connection.Storage, RelativeUri.Entries, UriParameter.EntryId, entryIdentifier.ToString(), UriParameter.EntriesWithRelation, entriesWithRelation.ToString(), UriParameter.EntryRelations, entryRelations.ToString())
            //var entries = Infrastructure.Get<IEnumerable<Entry>>(address)
            //return entries
        }
    }
}
