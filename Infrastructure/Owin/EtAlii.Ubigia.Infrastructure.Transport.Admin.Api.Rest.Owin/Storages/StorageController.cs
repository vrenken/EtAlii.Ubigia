namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.Admin
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Infrastructure.Functional;

    [Route(RelativeUri.Data.Storages)]
    public class StorageController : ApiController
    {
        private readonly IStorageRepository _items;

        //protected ILogger Logger { get { return _logger; } }
        //private readonly ILogger _logger;

        //private readonly IDiagnosticsConfiguration _diagnostics;

        public StorageController(IStorageRepository items)
        {
            _items = items;

            //_diagnostics = diagnostics;
            //if (_diagnostics.EnableLogging)
            //{
            //    Logger.Info("Creating StorageController");
            //}
        }

        protected override void Dispose(bool disposing)
        {
            //if (!disposing && _diagnostics.EnableLogging)
            //{
            //    Logger.Info("Destroying StorageController");
            //}
            base.Dispose(disposing);
        }

		//[Route(RelativeUri.Storages), HttpGet]
		[RequiresAuthenticationToken(Role.Admin, Role.User, Role.System)] // TODO: SECURITY: This is ugly and might be a security breach.
		public HttpResponseMessage GetLocal([FromUri]string local)
        {
            HttpResponseMessage response;
            try
            {
                var storage = _items.GetLocal();
                response = Request.CreateResponse(HttpStatusCode.OK, storage);
            }
            catch (Exception ex)
            {
                //Logger.Critical("Unable to serve a Storage GET client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

	    [RequiresAuthenticationToken(Role.Admin, Role.System)]
	    public HttpResponseMessage GetByName([FromUri]string storageName)
        {
            HttpResponseMessage response;
            try
            {
                var storage = _items.Get(storageName);
                response = Request.CreateResponse(HttpStatusCode.OK, storage);
            }
            catch (Exception ex)
            {
                //Logger.Critical("Unable to serve a Space GET client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }


		// Get all Items
		[RequiresAuthenticationToken(Role.Admin, Role.System)]
		public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                var items = _items.GetAll();
                response = Request.CreateResponse(HttpStatusCode.OK, items);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} GET client request", ex, typeof(T).Name);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

		// Get Item by id
		[RequiresAuthenticationToken(Role.Admin, Role.System)]
		public HttpResponseMessage Get([FromUri]Guid storageId)
        {
            HttpResponseMessage response;
            try
            {
                var item = _items.Get(storageId);
                response = Request.CreateResponse(HttpStatusCode.OK, item);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} GET client request", ex, typeof(T).Name);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

		// Add item
		[RequiresAuthenticationToken(Role.Admin, Role.System)]
		public HttpResponseMessage Post([FromBody]Storage item)
        {
            HttpResponseMessage response;
            try
            {
                item = _items.Add(item);
                response = Request.CreateResponse(HttpStatusCode.OK, item);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} POST client request", ex, typeof(T).Name);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

		// Update Item by id
		[RequiresAuthenticationToken(Role.Admin, Role.System)]
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
		[RequiresAuthenticationToken(Role.Admin, Role.System)]
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
