namespace EtAlii.Servus.Infrastructure.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport.WebApi;
    using EtAlii.Servus.Infrastructure.Functional;

    [RequiresAuthenticationToken]
    public class RootController : ApiController
    {
        private readonly IRootRepository _items;

        public RootController(
            IRootRepository items)
        {
            _items = items;
        }

        // Get all spaces for the specified accountid
        [Route(RelativeUri.Data.Roots), HttpGet]
        public HttpResponseMessage GetForSpace([FromUri]Guid spaceId)
        {
            HttpResponseMessage response;
            try
            {
                var roots = _items.GetAll(spaceId);
                response = Request.CreateResponse<IEnumerable<Root>>(HttpStatusCode.OK, roots);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Root GET client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }


        // Get Item by id
        [Route(RelativeUri.Data.Roots), HttpGet]
        public HttpResponseMessage GetById([FromUri]Guid spaceId, [FromUri]Guid rootId)
        {
            HttpResponseMessage response;
            try
            {
                var root = _items.Get(spaceId, rootId);
                response = Request.CreateResponse<Root>(HttpStatusCode.OK, root);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Root GET client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Get Item by id
        [Route(RelativeUri.Data.Roots), HttpGet]
        public HttpResponseMessage GetByName([FromUri]Guid spaceId, [FromUri]string rootName)
        {
            HttpResponseMessage response;
            try
            {
                var root = _items.Get(spaceId, rootName);
                response = Request.CreateResponse<Root>(HttpStatusCode.OK, root);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Root GET client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Add item
        [Route(RelativeUri.Data.Roots), HttpPost]
        public HttpResponseMessage Post([FromUri]Guid spaceId, [FromBody]Root root)
        {
            HttpResponseMessage response;
            try
            {
                root = _items.Add(spaceId, root);

                if (root == null)
                {
                    throw new InvalidOperationException("Unable to add root");
                }

                response = Request.CreateResponse<Root>(HttpStatusCode.OK, root);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Root POST client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Update Item by id
        [Route(RelativeUri.Data.Roots), HttpPut]
        public HttpResponseMessage Put([FromUri]Guid spaceId, Guid rootId, Root root)
        {
            HttpResponseMessage response;
            try
            {
                var result = _items.Update(spaceId, rootId, root);

                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Root PUT client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Delete Item by id
        [Route(RelativeUri.Data.Roots), HttpDelete]
        public HttpResponseMessage Delete([FromUri]Guid spaceId, [FromUri]Guid rootId)
        {
            HttpResponseMessage response;
            try
            {
                _items.Remove(spaceId, rootId);

                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Root DELETE client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }
    }
}
