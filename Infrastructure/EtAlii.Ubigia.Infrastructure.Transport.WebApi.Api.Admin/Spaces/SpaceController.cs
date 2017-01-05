namespace EtAlii.Ubigia.Infrastructure.Transport.WebApi.Api.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Infrastructure.Functional;

    [RequiresAuthenticationToken]
    [Route(RelativeUri.Data.Spaces)]
    public partial class SpaceController : ApiController
    {
        private readonly ISpaceRepository _items;

        //protected ILogger Logger { get { return _logger; } }
        //private readonly ILogger _logger;

        public SpaceController(ISpaceRepository items)
        {
            _items = items;
        }

        // Get all spaces for the specified accountid
        public HttpResponseMessage GetForAccount([FromUri]Guid accountId)
        {
            HttpResponseMessage response;
            try
            {
                var spaces = _items.GetAll(accountId);
                response = Request.CreateResponse<IEnumerable<Space>>(HttpStatusCode.OK, spaces);
            }
            catch (Exception ex)
            {
                //Logger.Critical("Unable to serve a Space GET client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        public HttpResponseMessage GetForAccount([FromUri]Guid accountId, [FromUri]string spaceName)
        {
            HttpResponseMessage response;
            try
            {
                var space = _items.Get(accountId, spaceName);
                response = Request.CreateResponse<Space>(HttpStatusCode.OK, space);
            }
            catch (Exception ex)
            {
                //Logger.Critical("Unable to serve a Space GET client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Get all Items
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                var items = _items.GetAll();
                response = Request.CreateResponse<IEnumerable<Space>>(HttpStatusCode.OK, items);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} GET client request", ex, typeof(T).Name);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Get Item by id
        public HttpResponseMessage Get([FromUri]Guid spaceId)
        {
            HttpResponseMessage response;
            try
            {
                var item = _items.Get(spaceId);
                response = Request.CreateResponse<Space>(HttpStatusCode.OK, item);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} GET client request", ex, typeof(T).Name);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Add item
        public HttpResponseMessage Post([FromBody]Space item, string spaceTemplate)
        {
            HttpResponseMessage response;
            try
            {
                var template = SpaceTemplate.All.Single(t => t.Name == spaceTemplate);
                item = _items.Add(item, template);
                response = Request.CreateResponse<Space>(HttpStatusCode.OK, item);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} POST client request", ex, typeof(T).Name);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Update Item by id
        public HttpResponseMessage Put([FromUri]Guid spaceId, Space space)
        {
            HttpResponseMessage response;
            try
            {
                var result = _items.Update(spaceId, space);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} PUT client request", ex, typeof(T).Name);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Delete Item by id
        public HttpResponseMessage Delete([FromUri]Guid spaceId)
        {
            HttpResponseMessage response;
            try
            {
                _items.Remove(spaceId);
                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} DELETE client request", ex, typeof(T).Name);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }
    }
}
