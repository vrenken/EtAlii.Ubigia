// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest
{
    using System;
    using System.Linq;
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
        public async Task<IActionResult> GetLocal([RequiredFromQuery]string local)
        {
            IActionResult response;
            try
            {
                var storage = await _items.GetLocal().ConfigureAwait(false);
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
        public async Task<IActionResult> GetByName([RequiredFromQuery]string storageName)
        {
            IActionResult response;
            try
            {
                var storage = await _items.Get(storageName).ConfigureAwait(false);
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
        public async Task<IActionResult> GetAll()
        {
            IActionResult response;
            try
            {
                var items = await _items
                    .GetAll()
                    .ToArrayAsync()
                    .ConfigureAwait(false);
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
        public async Task<IActionResult> GetById([RequiredFromQuery]Guid storageId)
        {
            IActionResult response;
            try
            {
                var item = await _items.Get(storageId).ConfigureAwait(false);
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
        public async Task<IActionResult> Put([RequiredFromQuery]Guid storageId, [FromBody]Storage storage)
        {
            IActionResult response;
            try
            {
                var result = await _items.Update(storageId, storage).ConfigureAwait(false);
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
        public async Task<IActionResult> Delete([RequiredFromQuery]Guid storageId)
        {
            IActionResult response;
            try
            {
                await _items.Remove(storageId).ConfigureAwait(false);
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
