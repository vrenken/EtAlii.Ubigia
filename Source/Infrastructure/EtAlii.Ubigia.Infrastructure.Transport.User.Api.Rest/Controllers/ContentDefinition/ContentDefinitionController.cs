// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest;

using System;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport.Rest;
using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.Ubigia.Infrastructure.Transport.Rest;
using Microsoft.AspNetCore.Mvc;

[RequiresAuthenticationToken(Role.User)]
[Route(RelativeDataUri.ContentDefinition)]
public class ContentDefinitionController : RestController
{
    private readonly IContentDefinitionRepository _items;

    public ContentDefinitionController(IContentDefinitionRepository items)
    {
        _items = items;
    }

    [HttpGet]
    public async Task<IActionResult> Get([RequiredFromQuery, ModelBinder(typeof(IdentifierBinder))]Identifier entryId)
    {
        IActionResult response;
        try
        {
            var contentDefinition = await _items
                .Get(entryId)
                .ConfigureAwait(false);
            response = Ok(contentDefinition);
        }
        catch (Exception ex)
        {
            response = BadRequest(ex.Message);
        }
        return response;
    }

    // Post a new ContentDefinition for the specified entry.
    [HttpPost]
    public async Task<IActionResult> Post([RequiredFromQuery, ModelBinder(typeof(IdentifierBinder))]Identifier entryId, [FromBody]ContentDefinition contentDefinition)
    {
        IActionResult response;
        try
        {
            // Store the ContentDefinition.
            await _items.Store(entryId, contentDefinition).ConfigureAwait(false);

            // Create the response.
            response = Ok();
        }
        catch (Exception ex)
        {
            response = BadRequest(ex.Message);
        }
        return response;
    }

    // Post a new ContentDefinitionPart for the specified entry.
    [HttpPut]
    public async Task<IActionResult> Put([RequiredFromQuery, ModelBinder(typeof(IdentifierBinder))]Identifier entryId, [RequiredFromQuery] ulong contentDefinitionPartId, [FromBody]ContentDefinitionPart contentDefinitionPart)
    {
        // Remark. We cannot have two post methods at the same time. The hosting
        // framework gets confused and does not out of the box know what method to choose.
        // Even if both have different parameters.
        // It might not be the best fit to alter this in PUT, but as the Rest interface
        // is the least important one this will do for now.
        // We've got bigger fish to fry.
        IActionResult response;
        try
        {
            if (contentDefinitionPartId != contentDefinitionPart.Id)
            {
                throw new InvalidOperationException("ContentDefinitionPartId does not match");
            }

            // Store the ContentDefinition.
            await _items
                .Store(entryId, contentDefinitionPart)
                .ConfigureAwait(false);

            // Create the response.
            response = Ok();
        }
        catch (Exception ex)
        {
            response = BadRequest(ex.Message);
        }
        return response;
    }
}
