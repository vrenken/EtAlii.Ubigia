namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.AspNetCore
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using Microsoft.AspNetCore.Mvc;

    //[RequiresAuthenticationToken]
    [Route(RelativeUri.Data.Storages)]
    public class StorageController : WebApiController
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
        [Route("{local}")]
        //public IActionResult GetLocal([FromUri]string local)
        public IActionResult GetLocal(string local)
        {
            IActionResult response;
            try
            {
                var storage = _items.GetLocal();
                response = Ok(storage);
            }
            catch (Exception ex)
            {
                //Logger.Critical("Unable to serve a Storage GET client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        [Route("{storageName}")]
        //public IActionResult GetByName([FromUri]string storageName)
        public IActionResult GetByName(string storageName)
        {
            IActionResult response;
            try
            {
                var storage = _items.Get(storageName);
                response = Ok(storage);
            }
            catch (Exception ex)
            {
                //Logger.Critical("Unable to serve a Space GET client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }


        // Get all Items
        public IActionResult Get()
        {
            IActionResult response;
            try
            {
                var items = _items.GetAll();
                response = Ok(items);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} GET client request", ex, typeof(T).Name);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Get Item by id
        [Route("{storageId}")]
        //public IActionResult Get([FromUri]Guid storageId)
        public IActionResult Get(Guid storageId)
        {
            IActionResult response;
            try
            {
                var item = _items.Get(storageId);
                response = Ok(item);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} GET client request", ex, typeof(T).Name);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Add item
        public IActionResult Post([FromBody]Storage item)
        {
            IActionResult response;
            try
            {
                item = _items.Add(item);
                response = Ok(item);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} POST client request", ex, typeof(T).Name);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Update Item by id
        [Route("{storageId}")]
        //public IActionResult Put([FromUri]Guid storageId, Storage storage)
        public IActionResult Put(Guid storageId, Storage storage)
        {
            IActionResult response;
            try
            {
                var result = _items.Update(storageId, storage);
                response = Ok(result);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} PUT client request", ex, typeof(T).Name);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Delete Item by id
        [Route("{storageId}")]
        //public IActionResult Delete([FromUri]Guid storageId)
        public IActionResult Delete(Guid storageId)
        {
            IActionResult response;
            try
            {
                _items.Remove(storageId);
                response = Ok();
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} DELETE client request", ex, typeof(T).Name);
                response = BadRequest(ex.Message);
            }
            return response;
        }
    }
}
