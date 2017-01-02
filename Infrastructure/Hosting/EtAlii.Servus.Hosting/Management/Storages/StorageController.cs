namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Infrastructure;
    using EtAlii.xTechnology.Logging;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    [RequiresAuthenticationToken]
    public class StorageController : ControllerBase<Storage, IStorageRepository>
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public StorageController(ILogger logger, IStorageRepository items, IDiagnosticsConfiguration diagnostics)
            : base(logger, items)
        {
            _diagnostics = diagnostics;
            if (_diagnostics.EnableLogging)
            {
                Logger.Info("Creating StorageController");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing && _diagnostics.EnableLogging)
            {
                Logger.Info("Destroying StorageController");
            }

            base.Dispose(disposing);
        }

        public HttpResponseMessage GetLocal([FromUri]string local)
        {
            HttpResponseMessage response;
            try
            {
                var storage = Items.GetLocal();
                response = Request.CreateResponse<Storage>(HttpStatusCode.OK, storage);
            }
            catch (Exception ex)
            {
                Logger.Critical("Unable to serve a Storage GET client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        public HttpResponseMessage GetByName([FromUri]string storageName)
        {
            HttpResponseMessage response;
            try
            {
                var storage = Items.Get(storageName);
                response = Request.CreateResponse<Storage>(HttpStatusCode.OK, storage);
            }
            catch (Exception ex)
            {
                Logger.Critical("Unable to serve a Space GET client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        public new HttpResponseMessage Get([FromUri]Guid storageId)
        {
            return base.Get(storageId);
        }

        public new HttpResponseMessage Put([FromUri]Guid storageId, Storage storage)
        {
            return base.Put(storageId, storage);
        }

        public new HttpResponseMessage Delete([FromUri]Guid storageId)
        {
            return base.Delete(storageId);
        }

    }
}
