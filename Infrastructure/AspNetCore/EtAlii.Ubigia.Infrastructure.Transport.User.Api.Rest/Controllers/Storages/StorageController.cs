﻿namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.NetCore;
    using Microsoft.AspNetCore.Mvc;

    //[RequiresAuthenticationToken]
    [Route(RelativeUri.User.Api.Storages)]
    public class StorageController : RestController
    {
        private readonly IStorageRepository _items;

        public StorageController(IStorageRepository items)
        {
            _items = items;
        }

	    //[AllowAnonymous]
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
    }
}
