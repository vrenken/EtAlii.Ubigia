namespace EtAlii.Servus.Infrastructure.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public abstract class ApiControllerBase<T, U> : ApiController
        where U : class, IRepository<T>
    {
        protected U Items { get { return _items; } }
        private readonly U _items;

        //protected ILogger Logger { get { return _logger; } }
        //private readonly ILogger _logger;

        protected ApiControllerBase(
            U items)
        {
            _items = items;
        }

        // Get all Items
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                var items = _items.GetAll();
                response = Request.CreateResponse<IEnumerable<T>>(HttpStatusCode.OK, items);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} GET client request", ex, typeof(T).Name);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Get Item by id
        protected HttpResponseMessage Get(Guid id)
        {
            HttpResponseMessage response;
            try
            {
                var item = _items.Get(id);
                response = Request.CreateResponse<T>(HttpStatusCode.OK, item);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} GET client request", ex, typeof(T).Name);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Add item
        public HttpResponseMessage Post([FromBody]T item)
        {
            HttpResponseMessage response;
            try
            {
                item = _items.Add(item);
                response = Request.CreateResponse<T>(HttpStatusCode.OK, item);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} POST client request", ex, typeof(T).Name);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Update Item by id
        protected HttpResponseMessage Put(Guid id, T item)
        {
            HttpResponseMessage response;
            try
            {
                var result = _items.Update(id, item);
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
        protected HttpResponseMessage Delete(Guid id)
        {
            HttpResponseMessage response;
            try
            {
                _items.Remove(id);
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
