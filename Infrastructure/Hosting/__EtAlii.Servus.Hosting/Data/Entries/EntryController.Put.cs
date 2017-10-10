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
        // Update Item by id
        [Route(RelativeUri.Entry), HttpPut]
        public HttpResponseMessage Put(Entry entry)
        {
            HttpResponseMessage response;
            try
            {
                // Store the entry.
                var result = _items.Store(entry);

                // Create the response.
                response = Request.CreateResponse(HttpStatusCode.OK, result);

                // Send the stord event.
                _hub.Stored(entry.Id);
            }
            catch (Exception ex)
            {
                _logger.Critical("Unable to serve a Entry PUT client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }
    }
}
