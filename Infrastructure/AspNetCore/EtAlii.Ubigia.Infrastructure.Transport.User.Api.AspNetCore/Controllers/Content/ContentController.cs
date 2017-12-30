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
    public class ContentController : WebApiController
    {
        private readonly IContentRepository _items;

        public ContentController(IContentRepository items)
        {
            _items = items;
        }

        [Route(RelativeUri.Data.Content + "/{entryId}"), HttpGet]
        //public IActionResult Get([FromUri(BinderType = typeof(IdentifierBinder))] Identifier entryId)
        public IActionResult Get([ModelBinder(typeof(IdentifierBinder))] Identifier entryId)
        {
            IActionResult response = null;
            try
            {
                var content = _items.Get(entryId);
                response = Ok((Content) content);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a content GET client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        [Route(RelativeUri.Data.Content + "/{entryId}/{contentPartId}"), HttpGet]
        //public IActionResult Get([FromUri(BinderType = typeof(IdentifierBinder))] Identifier entryId,
        //    [FromUri] UInt64 contentPartId)
        public IActionResult Get([ModelBinder(typeof(IdentifierBinder))] Identifier entryId, UInt64 contentPartId)
        {
            IActionResult response = null;
            try
            {
                var contentPart = _items.Get(entryId, contentPartId);
                response = Ok((ContentPart) contentPart);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Content part GET client request", ex);
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
        [Route(RelativeUri.Data.Content + "/{entryId}"), HttpPost]
        //public IActionResult Post([FromUri(BinderType = typeof(IdentifierBinder))] Identifier entryId, [FromBody] Content content)
        public IActionResult Post([ModelBinder(typeof(IdentifierBinder))] Identifier entryId, [FromBody] Content content)
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
                //_logger.Critical("Unable to serve a Content POST client request", ex);
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
        [Route(RelativeUri.Data.Content + "/{entryId}"), HttpPost]
        //public IActionResult Post([FromUri(BinderType = typeof(IdentifierBinder))] Identifier entryId, UInt64 contentPartId, [FromBody] ContentPart contentPart)
        public IActionResult Post([ModelBinder(typeof(IdentifierBinder))] Identifier entryId, UInt64 contentPartId, [FromBody] ContentPart contentPart)
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
                //_logger.Critical("Unable to serve a Content part POST client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }
    }
}
