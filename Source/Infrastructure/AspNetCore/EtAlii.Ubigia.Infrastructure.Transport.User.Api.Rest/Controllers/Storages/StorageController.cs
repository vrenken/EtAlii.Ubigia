// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Rest;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.Rest;
    using Microsoft.AspNetCore.Mvc;

    [RequiresAuthenticationToken(Role.User)]
    [Route(RelativeDataUri.Storages)]
    public class StorageController : RestController
    {
        private readonly IStorageRepository _items;

        public StorageController(IStorageRepository items)
        {
            _items = items;
        }

	    //[AllowAnonymous]
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
    }
}
