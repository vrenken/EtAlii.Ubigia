namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest.AspNetCore
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    //[RequiresAuthenticationToken]
    [Authorize]
    [Route(RelativeUri.User.Api.Content)]
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
            IActionResult response = null;
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
            IActionResult response = null;
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
            IActionResult response = null;
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
        [HttpPost]
        //public IActionResult Post([FromUri(BinderType = typeof(IdentifierBinder))] Identifier entryId, UInt64 contentPartId, [FromBody] ContentPart contentPart)
        public IActionResult Post([RequiredFromQuery, ModelBinder(typeof(IdentifierBinder))] Identifier entryId, [RequiredFromQuery] UInt64 contentPartId, [FromBody] ContentPart contentPart)
        {
            IActionResult response = null;
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
