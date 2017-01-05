namespace EtAlii.Ubigia.Infrastructure.Transport.SignalR
{
    using System;
    using EtAlii.Ubigia.Api;

    public partial class EntryHub : HubBase
    {
        // Get a new prepared entry for the specified spaceId
        public Entry Post(Guid spaceId)
        {
            Entry response;
            try
            {
                // Prepare the entry.
                response = _items.Prepare(spaceId);

                // Send the prepared event.
                SignalPrepared(response.Id);
            }
            catch (Exception e)
            {
                //_logger.Critical("Unable to serve a Entry POST client request", ex);
                throw new InvalidOperationException("Unable to serve a Entry POST client request", e);
            }
            return response;
        }
    }
}
