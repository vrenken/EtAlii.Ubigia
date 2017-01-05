namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.SignalR
{
    using System;
    using EtAlii.Ubigia.Api;

    public partial class EntryHub : HubBase
    {
        // Update Item by id
        public Entry Put(Entry entry)
        {
            Entry response;
            try
            {
                // Store the entry.
                response = _items.Store(entry);

                // Send the stord event.
                SignalStored(entry.Id);
            }
            catch (Exception e)
            {
                //_logger.Critical("Unable to serve a Entry PUT client request", ex);
                throw new InvalidOperationException("Unable to serve a Entry PUT client request", e);
            }
            return response;
        }
    }
}
