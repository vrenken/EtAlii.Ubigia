namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Infrastructure;
    using EtAlii.xTechnology.Logging;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    [RequiresAuthenticationToken]
    public class SpaceController : ControllerBase<Space, ISpaceRepository>
    {
        public SpaceController(ILogger logger, ISpaceRepository items)
            : base(logger, items)
        {
        }

        // Get all spaces for the specified accountid
        public HttpResponseMessage GetForAccount([FromUri]Guid accountId)
        {
            HttpResponseMessage response;
            try
            {
                var spaces = Items.GetAll(accountId);
                response = Request.CreateResponse<IEnumerable<Space>>(HttpStatusCode.OK, spaces);
            }
            catch (Exception ex)
            {
                Logger.Critical("Unable to serve a Space GET client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        public HttpResponseMessage GetForAccount([FromUri]Guid accountId, [FromUri]string spaceName)
        {
            HttpResponseMessage response;
            try
            {
                var space = Items.Get(accountId, spaceName);
                response = Request.CreateResponse<Space>(HttpStatusCode.OK, space);
            }
            catch (Exception ex)
            {
                Logger.Critical("Unable to serve a Space GET client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        public new HttpResponseMessage Get([FromUri]Guid spaceId)
        {
            return base.Get(spaceId);
        }

        public new HttpResponseMessage Put([FromUri]Guid spaceId, Space space)
        {
            return base.Put(spaceId, space);
        }

        public new HttpResponseMessage Delete([FromUri]Guid spaceId)
        {
            return base.Delete(spaceId);
        }
    }
}
