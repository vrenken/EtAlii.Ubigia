﻿namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest
{
    using System;
    using EtAlii.Ubigia.Api;
    using Microsoft.AspNetCore.Mvc;

    public partial class EntryController
    {
        // Update Item by id
        [HttpPut]
        public IActionResult Put([FromBody]Entry entry)
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
