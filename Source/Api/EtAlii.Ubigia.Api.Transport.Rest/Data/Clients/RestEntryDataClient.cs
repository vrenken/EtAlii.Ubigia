// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class RestEntryDataClient : RestClientBase, IEntryDataClient<IRestSpaceTransport>
    {
        public async Task<IEditableEntry> Prepare()
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Entry, UriParameter.SpaceId, Connection.Space.Id.ToString());
            var entry = await Connection.Client.Post<Entry>(address).ConfigureAwait(false);
            return entry;
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            var changeAddress = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Entry);
            var result = await Connection.Client.Put(changeAddress, entry as Entry).ConfigureAwait(false);
            scope.Cache.InvalidateEntry(entry.Id);
            // TODO: CACHING - Most probably the invalidateEntry could better be called on the result.id as well.
            //scope.Cache.InvalidateEntry(result.Id)
            return result;
        }

        public async Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope, EntryRelations entryRelations = EntryRelations.None)
        {
            return await Get(root.Identifier, scope, entryRelations).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope, EntryRelations entryRelations = EntryRelations.None)
        {
            return await scope.Cache.GetEntry(entryIdentifier, async () =>
            {

                var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Entry, UriParameter.EntryId,
                    entryIdentifier.ToString(), UriParameter.EntryRelations, entryRelations.ToString());
                var result = await Connection.Client.Get<Entry>(address).ConfigureAwait(false);
                return result;
            }).ConfigureAwait(false);
        }

        public async IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope, EntryRelations entryRelations = EntryRelations.None)
        {
            // Is it possible to improved this by using one single Web API call?
            // More details can be found in the Github issue below:
            // https://github.com/vrenken/EtAlii.Ubigia/issues/85

            foreach (var entryIdentifier in entryIdentifiers)
            {
                var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Entry, UriParameter.EntryId,
                    entryIdentifier.ToString(), UriParameter.EntryRelations, entryRelations.ToString());
                var entry = await Connection.Client.Get<Entry>(address).ConfigureAwait(false);
                yield return entry;
            }
        }

        public IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelations entriesWithRelation,
            ExecutionScope scope, EntryRelations entryRelations = EntryRelations.None)
        {
            return scope.Cache.GetRelatedEntries(entryIdentifier, entriesWithRelation, () => GetRelated(entryIdentifier, entriesWithRelation, entryRelations));
        }

        private async IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelations entriesWithRelation, EntryRelations entryRelations)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.RelatedEntries, UriParameter.EntryId, entryIdentifier.ToString(), UriParameter.EntriesWithRelation, entriesWithRelation.ToString(), UriParameter.EntryRelations, entryRelations.ToString());
            var result = await Connection.Client.Get<IEnumerable<Entry>>(address).ConfigureAwait(false);
            foreach (var item in result)
            {
                yield return item;
            }
        }
    }
}
