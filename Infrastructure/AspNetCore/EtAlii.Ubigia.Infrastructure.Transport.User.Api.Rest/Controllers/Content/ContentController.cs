﻿namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.NetCore;
    using Microsoft.AspNetCore.Mvc;

    [RequiresAuthenticationToken(Role.User)]
    [Route(RelativeUri.Data.Api.Content)]
    public class ContentController : RestController
    {
        private readonly IContentRepository _items;

        public ContentController(IContentRepository items)
        {
            _items = items;
        }

        [HttpGet]
        public IActionResult Get([RequiredFromQuery, ModelBinder(typeof(IdentifierBinder))] Identifier entryId)
        {
            IActionResult response;
            try
            {
                var content = _items.Get(entryId);
                response = Ok((Content) content);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }

        [HttpGet]
        public IActionResult Get([RequiredFromQuery, ModelBinder(typeof(IdentifierBinder))] Identifier entryId, [RequiredFromQuery]UInt64 contentPartId)
        {
            IActionResult response;
            try
            {
                var contentPart = _items.Get(entryId, contentPartId);
                response = Ok((ContentPart) contentPart);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }

        /// <summary>
        /// Post a new contentdefinition for the specified content.
        /// </summary>
        /// <param name="entryId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost] 
        public IActionResult Post([RequiredFromQuery, ModelBinder(typeof(IdentifierBinder))] Identifier entryId, [FromBody] Content content)
        {
            IActionResult response;
            try
            {
                // Store the content.
                _items.Store(entryId, content);

                // Create the response.
                response = Ok();
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }

        /// <summary>
        /// Post a new contentPart for the specified content.
        /// </summary>
        /// <param name="entryId"></param>
        /// <param name="contentPartId"></param>
        /// <param name="contentPart"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([RequiredFromQuery, ModelBinder(typeof(IdentifierBinder))] Identifier entryId, [RequiredFromQuery] UInt64 contentPartId, [FromBody] ContentPart contentPart)
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
                if (contentPartId != contentPart.Id)
                {
                    throw new InvalidOperationException("ContentPartId does not match");
                }

                // Store the content.
                _items.Store(entryId, contentPart);

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
