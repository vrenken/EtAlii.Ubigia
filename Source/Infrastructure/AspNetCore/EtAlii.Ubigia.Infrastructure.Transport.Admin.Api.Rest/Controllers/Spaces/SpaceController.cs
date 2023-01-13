// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest;

using System;
using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport.Rest;
using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.Ubigia.Infrastructure.Transport.Rest;
using Microsoft.AspNetCore.Mvc;

[RequiresAuthenticationToken(Role.Admin)]
[Route(RelativeManagementUri.Spaces)]
public class SpaceController : RestController
{
    private readonly ISpaceRepository _items;

    public SpaceController(ISpaceRepository items)
    {
        _items = items;
    }

    /// <summary>
    /// Get all spaces for the specified accountId.
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetForAccount([RequiredFromQuery]Guid accountId)
    {
        IActionResult response;
        try
        {
            var spaces = await _items
                .GetAll(accountId)
                .ToArrayAsync()
                .ConfigureAwait(false);
            response = Ok(spaces);
        }
        catch (Exception ex)
        {
            //Logger.Critical("Unable to serve a Space GET client request", ex)
            response = BadRequest(ex.Message);
        }
        return response;
    }

    [HttpGet]
    public async Task<IActionResult> GetForAccount([RequiredFromQuery]Guid accountId, [RequiredFromQuery]string spaceName)
    {
        IActionResult response;
        try
        {
            var space = await _items.Get(accountId, spaceName).ConfigureAwait(false);
            response = Ok(space);
        }
        catch (Exception ex)
        {
            //Logger.Critical("Unable to serve a Space GET client request", ex)
            response = BadRequest(ex.Message);
        }
        return response;
    }

    // Get all Items
    [HttpGet]
    public IActionResult Get()
    {
        IActionResult response;
        try
        {
            var items = _items.GetAll();
            response = Ok(items);
        }
        catch (Exception ex)
        {
            //Logger.Warning("Unable to serve a [0] GET client request", ex, typeof(T).Name)
            response = BadRequest(ex.Message);
        }
        return response;
    }

    // Get Item by id
    [HttpGet]
    public async Task<IActionResult> Get([RequiredFromQuery]Guid spaceId)
    {
        IActionResult response;
        try
        {
            var item = await _items.Get(spaceId).ConfigureAwait(false);
            response = Ok(item);
        }
        catch (Exception ex)
        {
            //Logger.Warning("Unable to serve a [0] GET client request", ex, typeof(T).Name)
            response = BadRequest(ex.Message);
        }
        return response;
    }

    // Add item
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]Space item, string spaceTemplate)
    {
        IActionResult response;
        try
        {
            var template = SpaceTemplate.All.Single(t => t.Name == spaceTemplate);
            item = await _items.Add(item, template).ConfigureAwait(false);
            response = Ok(item);
        }
        catch (Exception ex)
        {
            //Logger.Warning("Unable to serve a [0] POST client request", ex, typeof(T).Name)
            response = BadRequest(ex.Message);
        }
        return response;
    }

    // Update Item by id
    [HttpPut]
    public async Task<IActionResult> Put([RequiredFromQuery]Guid spaceId, [FromBody]Space space)
    {
        IActionResult response;
        try
        {
            var result = await _items.Update(spaceId, space).ConfigureAwait(false);
            response = Ok(result);
        }
        catch (Exception ex)
        {
            //Logger.Warning("Unable to serve a [0] PUT client request", ex, typeof(T).Name)
            response = BadRequest(ex.Message);
        }
        return response;
    }

    // Delete Item by id
    [HttpDelete]
    public async Task<IActionResult> Delete([RequiredFromQuery]Guid spaceId)
    {
        IActionResult response;
        try
        {
            await _items.Remove(spaceId).ConfigureAwait(false);
            response = Ok();
        }
        catch (Exception ex)
        {
            //Logger.Warning("Unable to serve a [0] DELETE client request", ex, typeof(T).Name)
            response = BadRequest(ex.Message);
        }
        return response;
    }
}
