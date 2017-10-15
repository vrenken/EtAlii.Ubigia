namespace EtAlii.Servus.Infrastructure.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Servus.Api;

    [RequiresAuthenticationToken]
    public partial class EntryDataHubProxy : HubProxyBase<EntryDataHub>
    {
        public Entry GetSingle(Identifier entryId, EntryRelation entryRelations = EntryRelation.None)
        {
            Entry response;
            try
            {
                response = _items.Get(entryId, entryRelations);
            }
            catch (Exception ex)
            {
                _logger.Critical("Unable to serve a Entry GET client request", ex);
                throw;
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
            catch (Exception ex)
            {
                _logger.Critical("Unable to serve a Entries GET client request", ex);
                throw;
            }
            return response;
        }

        public IEnumerable<Entry> GetRelated(Identifier entryId, EntryRelation entriesWithRelation, EntryRelation entryRelations = EntryRelation.None)
        {
            IEnumerable<Entry> response;
            try
            {
                response = _items.GetRelated(entryId, entriesWithRelation, entryRelations);
            }
            catch (Exception ex)
            {
                _logger.Critical("Unable to serve a ChildEntries GET client request", ex);
                throw;
            }
            return response;
        }
    }
}
