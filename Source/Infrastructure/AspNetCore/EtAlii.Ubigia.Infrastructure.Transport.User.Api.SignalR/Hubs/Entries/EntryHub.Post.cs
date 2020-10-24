﻿namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR
{
    using System;

    public partial class EntryHub
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
                throw new InvalidOperationException("Unable to serve a Entry POST client request", e);
            }
            return response;
        }
    }
}