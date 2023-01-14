// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR;

using System;
using System.Threading.Tasks;

public partial class EntryHub
{
    // Get a new prepared entry for the specified spaceId
    public async Task<Entry> Post(Guid spaceId)
    {
        Entry response;
        try
        {
            // Prepare the entry.
            response = await _items
                .Prepare(spaceId)
                .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Unable to serve a Entry POST client request", e);
        }
        return response;
    }
}
