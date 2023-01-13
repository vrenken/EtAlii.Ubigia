// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.SignalR;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Functional;

public class StorageHub : HubBase
{
    private readonly IStorageRepository _items;

    public StorageHub(
        IStorageRepository items,
        ISimpleAuthenticationTokenVerifier authenticationTokenVerifier)
        : base(authenticationTokenVerifier)
    {
        _items = items;
    }

    public async Task<Storage> GetLocal(string local)
    {
        Storage response;
        try
        {
            response = await _items.GetLocal().ConfigureAwait(false);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Unable to serve a Storage GET client request", e);
        }
        return response;
    }

    public async Task<Storage> GetByName(string storageName)
    {
        Storage response;
        try
        {
            response = await _items.Get(storageName).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Unable to serve a Storage GET client request", e);
        }
        return response;
    }


    // Get all Items
    public async IAsyncEnumerable<Storage> GetAll()
    {
        // The structure below might seem weird.
        // But it is not possible to combine a try-catch with the yield needed
        // enumerating an IAsyncEnumerable.
        // The only way to solve this is using the enumerator.
        var enumerator = _items
            .GetAll()
            .GetAsyncEnumerator();
        var hasResult = true;
        while (hasResult)
        {
            Storage item;
            try
            {
                hasResult = await enumerator
                    .MoveNextAsync()
                    .ConfigureAwait(false);
                item = hasResult ? enumerator.Current : null;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Storage GET client request", e);
            }
            if (item != null)
            {
                yield return item;
            }
        }
    }

    // Get Item by id
    public async Task<Storage> Get(Guid storageId)
    {
        Storage response;
        try
        {
            response = await _items.Get(storageId).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Unable to serve a Storage GET client request", e);
        }
        return response;
    }

    // Add item
    public async Task<Storage> Post(Storage item)
    {
        Storage response;
        try
        {
            response = await _items.Add(item).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Unable to serve a Storage POST client request", e);
        }
        return response;
    }

    // Update Item by id
    public async Task<Storage> Put(Guid storageId, Storage storage)
    {
        Storage response;
        try
        {
            response = await _items.Update(storageId, storage).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Unable to serve a Storage PUT client request", e);
        }
        return response;
    }

    // Delete Item by id
    public async Task Delete(Guid storageId)
    {
        try
        {
            await _items.Remove(storageId).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Unable to serve a Storage DELETE client request", e);
        }
    }
}
