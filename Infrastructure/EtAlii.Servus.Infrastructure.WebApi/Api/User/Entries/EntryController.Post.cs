namespace EtAlii.Servus.Infrastructure.WebApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport.WebApi;

    [RequiresAuthenticationToken]
    public partial class EntryController : ApiController
    {
        // Get a new prepared entry for the specified spaceId
        [Route(RelativeUri.Data.Entry), HttpPost]
        public HttpResponseMessage Post([FromUri]Guid spaceId)
        {
            HttpResponseMessage response;
            try
            {
                // Prepare the entry.
                var entry = _items.Prepare(spaceId);

                // Create the response.
                response = Request.CreateResponse<Entry>(HttpStatusCode.OK, entry);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Entry POST client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }
    }
}
