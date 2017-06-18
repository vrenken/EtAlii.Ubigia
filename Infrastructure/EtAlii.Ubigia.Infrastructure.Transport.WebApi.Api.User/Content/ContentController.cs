namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.User
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Infrastructure.Functional;

    [RequiresAuthenticationToken]
    public class ContentController : ApiController
    {
        private readonly IContentRepository _items;

        public ContentController(
            IContentRepository items)
        {
            _items = items;
        }

        [Route(RelativeUri.Data.Content), HttpGet]
        public HttpResponseMessage Get([FromUri(BinderType = typeof (IdentifierBinder))] Identifier entryId)
        {
            HttpResponseMessage response = null;
            try
            {
                var content = _items.Get(entryId);
                response = Request.CreateResponse(HttpStatusCode.OK, (Content) content);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a content GET client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        [Route(RelativeUri.Data.Content), HttpGet]
        public HttpResponseMessage Get([FromUri(BinderType = typeof (IdentifierBinder))] Identifier entryId,
            [FromUri] UInt64 contentPartId)
        {
            HttpResponseMessage response = null;
            try
            {
                var contentPart = _items.Get(entryId, contentPartId);
                response = Request.CreateResponse(HttpStatusCode.OK, (ContentPart) contentPart);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Content part GET client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        /// <summary>
        /// Post a new contentdefinition for the specified content.
        /// </summary>
        /// <param name="entryId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [Route(RelativeUri.Data.Content), HttpPost]
        public HttpResponseMessage Post([FromUri(BinderType = typeof (IdentifierBinder))] Identifier entryId, [FromBody] Content content)
        {
            HttpResponseMessage response = null;
            try
            {
                // Store the content.
                _items.Store(entryId, content);

                // Create the response.
                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Content POST client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
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
        [Route(RelativeUri.Data.Content), HttpPost]
        public HttpResponseMessage Post([FromUri(BinderType = typeof (IdentifierBinder))] Identifier entryId, UInt64 contentPartId, [FromBody] ContentPart contentPart)
        {
            HttpResponseMessage response = null;
            try
            {
                if (contentPartId != contentPart.Id)
                {
                    throw new InvalidOperationException("ContentPartId does not match");
                }

                // Store the content.
                _items.Store(entryId, contentPart);

                // Create the response.
                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Content part POST client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }
    }
}
