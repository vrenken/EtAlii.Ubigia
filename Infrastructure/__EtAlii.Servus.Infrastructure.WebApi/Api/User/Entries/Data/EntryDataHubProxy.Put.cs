namespace EtAlii.Servus.Infrastructure.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Servus.Api;

    [RequiresAuthenticationToken]
    public partial class EntryDataHubProxy : HubProxyBase<EntryDataHub>
    {
        // Update Item by id
        public Entry Put(Entry entry)
        {
            Entry response;
            try
            {
                // Store the entry.
                response = _items.Store(entry);
            }
            catch (Exception ex)
            {
                _logger.Critical("Unable to serve a Entry PUT client request", ex);
                throw;
            }
            return response;
        }
    }
}
