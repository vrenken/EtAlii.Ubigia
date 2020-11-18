namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public partial class EntryHub
    {
        public async Task<Entry> GetSingle(Identifier entryId, EntryRelation entryRelations = EntryRelation.None)
        {
            Entry response;
            try
            {
                response = await _items
                    .Get(entryId, entryRelations)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Entry GET client request", e);
            }
            return response;
        }

        public async Task<IEnumerable<Entry>> GetMultiple(IEnumerable<Identifier> entryIds, EntryRelation entryRelations = EntryRelation.None)
        {
            var response = new List<Entry>(); 
            try
            {
                foreach (var entryId in entryIds)
                {
                    var entry = await _items
                        .Get(entryId, entryRelations)
                        .ConfigureAwait(false);
                    response.Add(entry); // TODO: AsyncEnumerable 
                }
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
                    .ToArrayAsync()
                    .ConfigureAwait(false);   // TODO: AsyncEnumerable
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a related Entries GET client request", e);
            }
            return response;
        }
    }
}