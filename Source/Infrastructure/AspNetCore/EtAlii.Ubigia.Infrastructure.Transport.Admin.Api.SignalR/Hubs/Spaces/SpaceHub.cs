// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.SignalR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Functional;
using Microsoft.AspNetCore.SignalR;

public class SpaceHub : HubBase
{
    private readonly ISpaceRepository _items;
    private readonly IAccountRepository _accountItems;
    private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

    public SpaceHub(
        ISpaceRepository items,
        IAccountRepository accountItems,
        ISimpleAuthenticationTokenVerifier authenticationTokenVerifier,
        IAuthenticationTokenConverter authenticationTokenConverter)
        : base(authenticationTokenVerifier)
    {
        _items = items;
        _accountItems = accountItems;
        _authenticationTokenConverter = authenticationTokenConverter;
    }

    /// <summary>
    /// Get all spaces for the specified account id.
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async IAsyncEnumerable<Space> GetAllForAccount(Guid accountId)
    {
        // The structure below might seem weird.
        // But it is not possible to combine a try-catch with the yield needed
        // enumerating an IAsyncEnumerable.
        // The only way to solve this is using the enumerator.
        var enumerator = _items
            .GetAll(accountId)
            .GetAsyncEnumerator();
        var hasResult = true;
        while (hasResult)
        {
            Space item;
            try
            {
                hasResult = await enumerator
                    .MoveNextAsync()
                    .ConfigureAwait(false);
                item = hasResult ? enumerator.Current : null;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Space GET client request", e);
            }

            if (item != null)
            {
                yield return item;
            }
        }
    }

    public async Task<Space> GetForAccount(Guid accountId, string spaceName)
    {
        Space response;
        try
        {
            response = await _items.Get(accountId, spaceName).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Unable to serve a Space GET client request", e);
        }
        return response;
    }

    public async Task<Space> GetForAuthenticationToken(string spaceName)
    {
        Space response;
        try
        {
            var httpContext = Context.GetHttpContext();
            httpContext!.Request.Headers.TryGetValue("Authentication-Token", out var stringValues);
            var authenticationTokenAsString = stringValues.Single();
            var authenticationToken = _authenticationTokenConverter.FromString(authenticationTokenAsString);

            var account = await _accountItems.Get(authenticationToken.Name).ConfigureAwait(false);

            response = await _items.Get(account.Id, spaceName).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Unable to serve a Space GET client request", e);
        }
        return response;
    }

    // Get all Items
    public async IAsyncEnumerable<Space> GetAll()
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
            Space item;
            try
            {
                hasResult = await enumerator
                    .MoveNextAsync()
                    .ConfigureAwait(false);
                item = hasResult ? enumerator.Current : null;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Space GET client request", e);
            }
            if (item != null)
            {
                yield return item;
            }
        }
    }

    // Get Item by id
    public async Task<Space> Get(Guid spaceId)
    {
        Space response;
        try
        {
            response = await _items.Get(spaceId).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Unable to serve a Space GET client request", e);
        }
        return response;
    }

    // Add item
    public async Task<Space> Post(Space item, string spaceTemplate)
    {
        Space response;
        try
        {
            var template = SpaceTemplate.All.Single(t => t.Name == spaceTemplate);
            response = await _items.Add(item, template).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Unable to serve a Space POST client request", e);
        }
        return response;
    }

    // Update Item by id
    public async Task<Space> Put(Guid spaceId, Space space)
    {
        Space response;
        try
        {
            response = await _items.Update(spaceId, space).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Unable to serve a Space PUT client request", e);
        }
        return response;
    }

    // Delete Item by id
    public async Task Delete(Guid spaceId)
    {
        try
        {
            await _items.Remove(spaceId).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Unable to serve a Space DELETE client request", e);
        }
    }
}
