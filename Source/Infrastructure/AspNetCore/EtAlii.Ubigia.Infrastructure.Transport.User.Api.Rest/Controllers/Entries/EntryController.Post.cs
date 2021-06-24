// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Transport.Rest;
    using Microsoft.AspNetCore.Mvc;

    public partial class EntryController
    {
        // Get a new prepared entry for the specified spaceId
        [HttpPost]
        public async Task<IActionResult> Post([RequiredFromQuery]Guid spaceId)
        {
            IActionResult response;
            try
            {
                // Prepare the entry.
                var entry = await _items
                    .Prepare(spaceId)
                    .ConfigureAwait(false);

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
