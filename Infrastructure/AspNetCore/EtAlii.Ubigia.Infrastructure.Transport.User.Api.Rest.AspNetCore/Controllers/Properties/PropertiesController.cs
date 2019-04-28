﻿namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest.AspNetCore
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    //[RequiresAuthenticationToken]
    [Authorize]
    [Route(RelativeUri.User.Api.Properties)]
    public class PropertiesController : RestController
    {
        private readonly IPropertiesRepository _properties;

        public PropertiesController(IPropertiesRepository properties)
        {
            _properties = properties;
        }

        [HttpGet]
        public IActionResult Get([RequiredFromQuery, ModelBinder(typeof(IdentifierBinder))]Identifier entryId)
        {
            IActionResult response = null;
            try
            {
                var properties = _properties.Get(entryId);
                response = Ok(properties);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }

        /// <summary>
        /// Post a new contentdefinition for the specified content.
        /// </summary>
        /// <param name="entryId"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([RequiredFromQuery, ModelBinder(typeof(IdentifierBinder))]Identifier entryId, [FromBody]PropertyDictionary properties)
        {
            IActionResult response = null;
            try
            {
                // Store the content.
                _properties.Store(entryId, properties);

                // Create the response.
                response = Ok();
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }
    }
}
