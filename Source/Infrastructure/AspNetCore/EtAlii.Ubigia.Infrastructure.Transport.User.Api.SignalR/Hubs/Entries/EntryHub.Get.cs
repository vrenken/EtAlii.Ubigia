// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class EntryHub
{
    public async Task<Entry> GetSingle(Identifier entryId, EntryRelations entryRelations = EntryRelations.None)
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

    public async IAsyncEnumerable<Entry> GetMultiple(IEnumerable<Identifier> entryIds, EntryRelations entryRelations = EntryRelations.None)
    {
        // The structure below might seem weird.
        // But it is not possible to combine a try-catch with the yield needed
        // enumerating an IAsyncEnumerable.
        // The only way to solve this is using the enumerator.
        using var enumerator = entryIds.GetEnumerator();
        var hasResult = true;
        while (hasResult)
        {
            Entry item;
            try
            {
                hasResult = enumerator.MoveNext();
                item = hasResult
                    ? await _items
                        .Get(enumerator.Current, entryRelations)
                        .ConfigureAwait(false)
                    : null;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Entries GET client request", e);
            }
            if (item != null)
            {
                yield return item;
            }
        }
    }

    public async IAsyncEnumerable<Entry> GetRelated(Identifier entryId, EntryRelations entriesWithRelation, EntryRelations entryRelations = EntryRelations.None)
    {
        // The structure below might seem weird.
        // But it is not possible to combine a try-catch with the yield needed
        // enumerating an IAsyncEnumerable.
        // The only way to solve this is using the enumerator.
        var enumerator = _items
            .GetRelated(entryId, entriesWithRelation, entryRelations)
            .GetAsyncEnumerator();
        var hasResult = true;
        while (hasResult)
        {
            Entry item;
            try
            {
                hasResult = await enumerator
                    .MoveNextAsync()
                    .ConfigureAwait(false);
                item = hasResult ? enumerator.Current : null;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a related Entries GET client request", e);
            }
            if (item != null)
            {
                yield return item;
            }
        }
    }
}
