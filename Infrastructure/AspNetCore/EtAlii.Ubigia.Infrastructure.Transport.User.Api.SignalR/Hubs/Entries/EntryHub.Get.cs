namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public partial class EntryHub
    {
        public Entry GetSingle(Identifier entryId, EntryRelation entryRelations = EntryRelation.None)
        {
            Entry response;
            try
            {
                response = _items.Get(entryId, entryRelations);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Entry GET client request", e);
            }
            return response;
        }

        public IEnumerable<Entry> GetMultiple(IEnumerable<Identifier> entryIds, EntryRelation entryRelations = EntryRelation.None)
        {
            Entry[] response;
            try
            {
                response = entryIds.Select(entryId => _items.Get(entryId, entryRelations)).ToArray();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Entries GET client request", e);
            }
            return response;
        }

        public async Task<IEnumerable<Entry>> GetRelated(Identifier entryId, EntryRelation entriesWithRelation, EntryRelation entryRelations = EntryRelation.None)
        {
            IEnumerable<Entry> response;
            try
            {
                response = await _items
                    .GetRelated(entryId, entriesWithRelation, entryRelations)
                    .ToArrayAsync();   // TODO: AsyncEnumerable
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a related Entries GET client request", e);
            }
            return response;
        }
    }
}