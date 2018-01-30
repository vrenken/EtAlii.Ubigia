namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.User
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Infrastructure.Functional;

    [RequiresAuthenticationToken(Role.User, Role.System)]
    public class ContentDefinitionController : ApiController
    {
        private readonly IContentDefinitionRepository _items;

        public ContentDefinitionController(
            IContentDefinitionRepository items)
        {
            _items = items;
        }

        [Route(RelativeUri.Data.ContentDefinition), HttpGet]
        public HttpResponseMessage Get([FromUri(BinderType = typeof(IdentifierBinder))]Identifier entryId)
        {
            HttpResponseMessage response = null;
            try
            {
                var contentDefinition = _items.Get(entryId);
                response = Request.CreateResponse(HttpStatusCode.OK, (ContentDefinition)contentDefinition);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a ContentDefinition GET client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Post a new contentdefinition for the specified entry.
        [Route(RelativeUri.Data.ContentDefinition), HttpPost]
        public HttpResponseMessage Post([FromUri(BinderType = typeof(IdentifierBinder))]Identifier entryId, [FromBody]ContentDefinition contentDefinition)
        {
            HttpResponseMessage response = null;
            try
            {
                // Store the ContentDefinition.
                _items.Store(entryId, contentDefinition);

                // Create the response.
                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a ContentDefinition POST client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Post a new ContentDefinitionPart for the specified entry.
        [Route(RelativeUri.Data.ContentDefinition), HttpPost]
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
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a ContentDefinition POST client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }
    }
}
