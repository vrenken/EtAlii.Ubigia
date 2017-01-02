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
    public class ContentDefinitionController : ApiController
    {
        private readonly ContentDefinitionHubServerProxy _hub;
        private readonly ILogger _logger;
        private readonly IContentDefinitionRepository _items;

        public ContentDefinitionController(
            ILogger logger, 
            IContentDefinitionRepository items,
            ContentDefinitionHubServerProxy hub)
        {
            _hub = hub;
            _logger = logger;
            _items = items;
        }

        [Route(RelativeUri.ContentDefinition), HttpGet]
        public HttpResponseMessage Get([FromUri(BinderType = typeof(IdentifierBinder))]Identifier entryId)
        {
            HttpResponseMessage response = null;
            try
            {
                var contentDefinition = _items.Get(entryId);
                response = Request.CreateResponse<ContentDefinition>(HttpStatusCode.OK, (ContentDefinition)contentDefinition);
            }
            catch (Exception ex)
            {
                _logger.Critical("Unable to serve a ContentDefinition GET client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Post a new contentdefinition for the specified entry.
        [Route(RelativeUri.ContentDefinition), HttpPost]
        public HttpResponseMessage Post([FromUri(BinderType = typeof(IdentifierBinder))]Identifier entryId, [FromBody]ContentDefinition contentDefinition)
        {
            HttpResponseMessage response = null;
            try
            {
                // Store the ContentDefinition.
                _items.Store(entryId, contentDefinition);

                // Create the response.
                response = Request.CreateResponse(HttpStatusCode.OK);

                // Send the updated event.
                _hub.Updated(entryId);
            }
            catch (Exception ex)
            {
                _logger.Critical("Unable to serve a ContentDefinition POST client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Post a new ContentDefinitionPart for the specified entry.
        [Route(RelativeUri.ContentDefinition), HttpPost]
        public HttpResponseMessage Post([FromUri(BinderType = typeof(IdentifierBinder))]Identifier entryId, UInt64 contentDefinitionPartId, [FromBody]ContentDefinitionPart contentDefinitionPart)
        {
            HttpResponseMessage response = null;
            try
            {
                if (contentDefinitionPartId != contentDefinitionPart.Id)
                {
                    throw new InvalidOperationException("ContentDefinitionPartId does not match");
                }

                // Store the ContentDefinition.
                _items.Store(entryId, contentDefinitionPart);

                // Create the response.
                response = Request.CreateResponse(HttpStatusCode.OK);

                // Send the updated event.
                _hub.Updated(entryId);
            }
            catch (Exception ex)
            {
                _logger.Critical("Unable to serve a ContentDefinition POST client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

    }
}
