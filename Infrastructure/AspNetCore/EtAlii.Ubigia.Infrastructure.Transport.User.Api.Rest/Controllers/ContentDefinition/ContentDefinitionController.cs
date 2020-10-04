namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest
{
    using System;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.NetCore;
    using Microsoft.AspNetCore.Mvc;

    [RequiresAuthenticationToken(Role.User)]
    [Route(RelativeUri.Data.Api.ContentDefinition)]
    public class ContentDefinitionController : RestController
    {
        private readonly IContentDefinitionRepository _items;

        public ContentDefinitionController(IContentDefinitionRepository items)
        {
            _items = items;
        }

        [HttpGet]
        public IActionResult Get([RequiredFromQuery, ModelBinder(typeof(IdentifierBinder))]Identifier entryId)
        {
            IActionResult response;
            try
            {
                var contentDefinition = _items.Get(entryId);
                response = Ok((ContentDefinition)contentDefinition);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Post a new contentdefinition for the specified entry.
        [HttpPost]
        public IActionResult Post([RequiredFromQuery, ModelBinder(typeof(IdentifierBinder))]Identifier entryId, [FromBody]ContentDefinition contentDefinition)
        {
            IActionResult response;
            try
            {
                // Store the ContentDefinition.
                _items.Store(entryId, contentDefinition);

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
        public IActionResult Put([RequiredFromQuery, ModelBinder(typeof(IdentifierBinder))]Identifier entryId, [RequiredFromQuery] UInt64 contentDefinitionPartId, [FromBody]ContentDefinitionPart contentDefinitionPart)
        {
            // Remark. We cannot have two post methods at the same time. The hosting 
            // framework gets confused and does not out of the box know what method to choose.
            // Even if both have different parameters.
            // It might not be the best fit to alter this in PUT, but as the WebApi interface
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
                _items.Store(entryId, contentDefinitionPart);

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
}
