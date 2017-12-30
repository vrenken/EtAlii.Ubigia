namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.AspNetCore
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    //[RequiresAuthenticationToken]
    [Authorize]
    public class ContentDefinitionController : WebApiController
    {
        private readonly IContentDefinitionRepository _items;

        public ContentDefinitionController(IContentDefinitionRepository items)
        {
            _items = items;
        }

        [Route(RelativeUri.Data.ContentDefinition + "/{entryId}"), HttpGet]
        //public IActionResult Get([FromUri(BinderType = typeof(IdentifierBinder))]Identifier entryId)
        public IActionResult Get([ModelBinder(typeof(IdentifierBinder))]Identifier entryId)
        {
            IActionResult response = null;
            try
            {
                var contentDefinition = _items.Get(entryId);
                response = Ok((ContentDefinition)contentDefinition);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a ContentDefinition GET client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Post a new contentdefinition for the specified entry.
        [Route(RelativeUri.Data.ContentDefinition + "/{entryId}"), HttpPost]
        //public IActionResult Post([FromUri(BinderType = typeof(IdentifierBinder))]Identifier entryId, [FromBody]ContentDefinition contentDefinition)
        public IActionResult Post([ModelBinder(typeof(IdentifierBinder))]Identifier entryId, [FromBody]ContentDefinition contentDefinition)
        {
            IActionResult response = null;
            try
            {
                // Store the ContentDefinition.
                _items.Store(entryId, contentDefinition);

                // Create the response.
                response = Ok();
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a ContentDefinition POST client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Post a new ContentDefinitionPart for the specified entry.
        [Route(RelativeUri.Data.ContentDefinition + "/{entryId}"), HttpPost]
        //public IActionResult Post([FromUri(BinderType = typeof(IdentifierBinder))]Identifier entryId, UInt64 contentDefinitionPartId, [FromBody]ContentDefinitionPart contentDefinitionPart)
        public IActionResult Post([ModelBinder(typeof(IdentifierBinder))]Identifier entryId, UInt64 contentDefinitionPartId, [FromBody]ContentDefinitionPart contentDefinitionPart)
        {
            IActionResult response = null;
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
                //_logger.Critical("Unable to serve a ContentDefinition POST client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }
    }
}
