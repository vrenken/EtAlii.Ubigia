namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.AspNetCore
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    //[RequiresAuthenticationToken]
    [Authorize]
    public partial class EntryController : WebApiController
    {
        // Get a new prepared entry for the specified spaceId
        [HttpPost]
        public IActionResult Post([RequiredFromQuery]Guid spaceId)
        {
            IActionResult response;
            try
            {
                // Prepare the entry.
                var entry = _items.Prepare(spaceId);

                // Create the response.
                response = Ok(entry);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }
    }
}
