namespace EtAlii.Servus.Infrastructure.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Servus.Api;

    [RequiresAuthenticationToken]
    public partial class EntryDataHubProxy : HubProxyBase<EntryDataHub>
    {
        // Get a new prepared entry for the specified spaceId
        public Entry Post(Guid spaceId)
        {
            Entry response;
            try
            {
                // Prepare the entry.
                response = _items.Prepare(spaceId);
            }
            catch (Exception ex)
            {
                _logger.Critical("Unable to serve a Entry POST client request", ex);
                throw;
            }
            return response;
        }
    }
}
