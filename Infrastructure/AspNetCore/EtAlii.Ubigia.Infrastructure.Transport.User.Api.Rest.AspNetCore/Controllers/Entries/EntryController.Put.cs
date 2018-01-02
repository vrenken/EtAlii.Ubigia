namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest.AspNetCore
{
    using System;
    using EtAlii.Ubigia.Api;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    //[RequiresAuthenticationToken]
    [Authorize]
    public partial class EntryController : RestController
    {
        // Update Item by id
        [HttpPut]
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
                response = BadRequest(ex.Message);
            }
            return response;
        }
    }
}
