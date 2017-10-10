namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.Servus.Infrastructure;
    using EtAlii.xTechnology.Logging;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    [RequiresAuthenticationToken]
    public class ContentController : ApiController
    {
        private readonly ContentHubServerProxy _hub;
        private readonly ILogger _logger;
        private readonly IContentRepository _items;

        public ContentController(
            ILogger logger, 
            IContentRepository items,
            ContentHubServerProxy hub)
        {
            _hub = hub;
            _logger = logger;
            _items = items;
        }

        [Route(RelativeUri.Content), HttpGet]
        public HttpResponseMessage Get([FromUri(BinderType = typeof(IdentifierBinder))]Identifier entryId)
        {
            HttpResponseMessage response = null;
            try
            {
                var content = _items.Get(entryId);
                response = Request.CreateResponse<Content>(HttpStatusCode.OK, (Content)content);
            }
            catch (Exception ex)
            {
                _logger.Critical("Unable to serve a content GET client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        [Route(RelativeUri.Content), HttpGet]
        public HttpResponseMessage Get([FromUri(BinderType = typeof(IdentifierBinder))]Identifier entryId, [FromUri]UInt64 contentPartId)
        {
            HttpResponseMessage response = null;
            try
            {
                var contentPart = _items.Get(entryId, contentPartId);
                response = Request.CreateResponse<ContentPart>(HttpStatusCode.OK, (ContentPart)contentPart);
            }
            catch (Exception ex)
            {
                _logger.Critical("Unable to serve a Content part GET client request", ex);
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
        [Route(RelativeUri.Content), HttpPost]
        public HttpResponseMessage Post([FromUri(BinderType = typeof(IdentifierBinder))]Identifier entryId, [FromBody]Content content)
        {
            HttpResponseMessage response = null;
            try
            {
                // Store the content.
                _items.Store(entryId, content);

                // Create the response.
                response = Request.CreateResponse(HttpStatusCode.OK);

                // Send the updated event.
                _hub.Updated(entryId);
            }
            catch (Exception ex)
            {
                _logger.Critical("Unable to serve a Content POST client request", ex);
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
        [Route(RelativeUri.Content), HttpPost]
        public HttpResponseMessage Post([FromUri(BinderType = typeof(IdentifierBinder))]Identifier entryId, UInt64 contentPartId, [FromBody]ContentPart contentPart)
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

                // Send the updated event.
                _hub.Updated(entryId);
            }
            catch (Exception ex)
            {
                _logger.Critical("Unable to serve a Content part POST client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }
}
}
