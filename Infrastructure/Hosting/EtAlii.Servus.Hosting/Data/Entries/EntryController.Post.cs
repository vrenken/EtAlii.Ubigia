namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Api;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Fabric;

    [RequiresAuthenticationToken]
    public partial class EntryController : ApiController
    {
        // Get a new prepared entry for the specified spaceId
        [Route(RelativeUri.Entry), HttpPost]
        public HttpResponseMessage Post([FromUri]Guid spaceId)
        {
            HttpResponseMessage response;
            try
            {
                // Prepare the entry.
                var entry = _items.Prepare(spaceId);

                // Create the response.
                response = Request.CreateResponse<Entry>(HttpStatusCode.OK, entry);

                // Send the prepared event.
                _hub.Prepared(entry.Id);
            }
            catch (Exception ex)
            {
                _logger.Critical("Unable to serve a Entry POST client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }
    }
}
