namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.AspNetCore
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    //[RequiresAuthenticationToken]
    [Authorize]
    public partial class EntryController : WebApiController
    {
        // Update Item by id
        [Route(RelativeUri.Data.Entry), HttpPut]
        public IActionResult Put(Entry entry)
        {
            IActionResult response;
            try
            {
                // Store the entry.
                var result = _items.Store(entry);

                // Create the response.
                response = Ok(result);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Entry PUT client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }
    }
}
