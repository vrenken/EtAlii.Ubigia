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
    public class PropertiesController : ApiController
    {
        private readonly IPropertiesRepository _properties;

        public PropertiesController(
            IPropertiesRepository properties)
        {
            _properties = properties;
        }

        [Route(RelativeUri.Data.Properties), HttpGet]
        public HttpResponseMessage Get([FromUri(BinderType = typeof(IdentifierBinder))]Identifier entryId)
        {
            HttpResponseMessage response = null;
            try
            {
                var properties = _properties.Get(entryId);
                response = Request.CreateResponse(HttpStatusCode.OK, properties);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a properties GET client request", ex);
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
        [Route(RelativeUri.Data.Properties), HttpPost]
        public HttpResponseMessage Post([FromUri(BinderType = typeof(IdentifierBinder))]Identifier entryId, [FromBody]PropertyDictionary properties)
        {
            HttpResponseMessage response = null;
            try
            {
                // Store the content.
                _properties.Store(entryId, properties);

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
    }
}
