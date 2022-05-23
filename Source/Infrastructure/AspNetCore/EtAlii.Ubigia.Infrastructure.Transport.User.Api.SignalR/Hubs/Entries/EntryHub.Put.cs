// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR
{
    using System;
    using System.Threading.Tasks;

    public partial class EntryHub
    {
        // Update Item by id
        public async Task<Entry> Put(Entry entry)
        {
            Entry response;
            try
            {
                // Store the entry.
                response = await _items.Store(entry).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Entry PUT client request", e);
            }
            return response;
        }
    }
}
