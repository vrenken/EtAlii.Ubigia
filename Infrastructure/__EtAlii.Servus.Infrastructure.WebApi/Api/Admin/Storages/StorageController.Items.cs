namespace EtAlii.Servus.Infrastructure.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using EtAlii.Servus.Api;

    public partial class StorageController : ApiController
    {
        private readonly IStorageRepository _items;

        //protected ILogger Logger { get { return _logger; } }
        //private readonly ILogger _logger;

        // Get all Items
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                var items = _items.GetAll();
                response = Request.CreateResponse<IEnumerable<Storage>>(HttpStatusCode.OK, items);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} GET client request", ex, typeof(T).Name);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Get Item by id
        public HttpResponseMessage Get([FromUri]Guid storageId)
        {
            HttpResponseMessage response;
            try
            {
                var item = _items.Get(storageId);
                response = Request.CreateResponse<Storage>(HttpStatusCode.OK, item);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} GET client request", ex, typeof(T).Name);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Add item
        public HttpResponseMessage Post([FromBody]Storage item)
        {
            HttpResponseMessage response;
            try
            {
                item = _items.Add(item);
                response = Request.CreateResponse<Storage>(HttpStatusCode.OK, item);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} POST client request", ex, typeof(T).Name);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Update Item by id
        public HttpResponseMessage Put([FromUri]Guid storageId, Storage storage)
        {
            HttpResponseMessage response;
            try
            {
                var result = _items.Update(storageId, storage);
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
        public HttpResponseMessage Delete([FromUri]Guid storageId)
        {
            HttpResponseMessage response;
            try
            {
                _items.Remove(storageId);
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
