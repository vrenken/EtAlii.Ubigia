namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest.AspNetCore
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    //[RequiresAuthenticationToken]
    //[Authorize]
    [Route(RelativeUri.Admin.Api.Storages)]
    public class StorageController : RestController
    {
        private readonly IStorageRepository _items;

        public StorageController(IStorageRepository items)
        {
            _items = items;
        }

        [HttpGet]
        public IActionResult GetLocal([RequiredFromQuery]string local)
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

        [HttpGet]
        public IActionResult GetByName([RequiredFromQuery]string storageName)
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
        [HttpGet]
        public IActionResult GetAll()
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
        [HttpGet]
        public IActionResult GetById([RequiredFromQuery]Guid storageId)
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
        [HttpPost]
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
        [HttpPut]
        public IActionResult Put([RequiredFromQuery]Guid storageId, [FromBody]Storage storage)
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
        [HttpDelete]
        public IActionResult Delete([RequiredFromQuery]Guid storageId)
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
