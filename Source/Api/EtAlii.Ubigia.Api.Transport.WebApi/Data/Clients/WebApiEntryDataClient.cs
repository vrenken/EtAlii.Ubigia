﻿namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class WebApiEntryDataClient : WebApiClientBase, IEntryDataClient<IWebApiSpaceTransport>
    {
        public async Task<IEditableEntry> Prepare()
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeUri.Data.Entry, UriParameter.SpaceId, Connection.Space.Id.ToString());
            var entry = await Connection.Client.Post<Entry>(address);
            return entry;
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            var changeAddress = Connection.AddressFactory.Create(Connection.Transport, RelativeUri.Data.Entry);
            var result = await Connection.Client.Put(changeAddress, entry as Entry);
            scope.Cache.InvalidateEntry(entry.Id);
            // TODO: CACHING - Most probably the invalidateEntry could better be called on the result.id as well.
            //scope.Cache.InvalidateEntry(result.Id)
            return result;
        }

        public async Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return await Get(root.Identifier, scope, entryRelations);
        }

        public async Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return await scope.Cache.GetEntry(entryIdentifier, async () =>
            {

                var address = Connection.AddressFactory.Create(Connection.Transport, RelativeUri.Data.Entry, UriParameter.EntryId,
                    entryIdentifier.ToString(), UriParameter.EntryRelations, entryRelations.ToString());
                var result = await Connection.Client.Get<Entry>(address);
                return result;
            });
        }

        public async IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            // TODO: this can be improved by using one single Web API call.
            foreach (var entryIdentifier in entryIdentifiers)
            {
                var address = Connection.AddressFactory.Create(Connection.Transport, RelativeUri.Data.Entry, UriParameter.EntryId,
                    entryIdentifier.ToString(), UriParameter.EntryRelations, entryRelations.ToString());
                var entry = await Connection.Client.Get<Entry>(address);
                yield return entry;
            }
        }

        public IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation,
            ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            return scope.Cache.GetRelatedEntries(entryIdentifier, entriesWithRelation, () => GetRelated(entryIdentifier, entriesWithRelation, entryRelations));
        }

        private async IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier,EntryRelation entriesWithRelation, EntryRelation entryRelations)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeUri.Data.RelatedEntries, UriParameter.EntryId, entryIdentifier.ToString(), UriParameter.EntriesWithRelation, entriesWithRelation.ToString(), UriParameter.EntryRelations, entryRelations.ToString());
            var result = await Connection.Client.Get<IEnumerable<Entry>>(address);
            foreach (var item in result)
            {
                yield return item;
            }
        }
    }
}