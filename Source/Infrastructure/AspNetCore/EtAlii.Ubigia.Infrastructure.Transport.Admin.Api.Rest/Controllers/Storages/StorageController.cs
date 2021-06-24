// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Rest;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.Rest;
    using Microsoft.AspNetCore.Mvc;

    [RequiresAuthenticationToken(Role.Admin)]
    [Route(RelativeManagementUri.Storages)]
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
                //Logger.Critical("Unable to serve a Storage GET client request", ex)
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
                //Logger.Critical("Unable to serve a Space GET client request", ex)
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
                //Logger.Warning("Unable to serve a [0] GET client request", ex, typeof(T).Name)
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
                //Logger.Warning("Unable to serve a [0] GET client request", ex, typeof(T).Name)
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Add item
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Storage item)
        {
            IActionResult response;
            try
            {
                item = await _items.Add(item).ConfigureAwait(false);
                response = Ok(item);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a [0] POST client request", ex, typeof(T).Name)
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
                //Logger.Warning("Unable to serve a [0] PUT client request", ex, typeof(T).Name)
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
                //Logger.Warning("Unable to serve a [0] DELETE client request", ex, typeof(T).Name)
                response = BadRequest(ex.Message);
            }
            return response;
        }
    }
}
