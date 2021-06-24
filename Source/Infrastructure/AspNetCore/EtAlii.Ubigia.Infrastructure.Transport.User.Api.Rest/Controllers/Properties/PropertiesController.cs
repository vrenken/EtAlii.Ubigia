// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest
{
    using System;
    using EtAlii.Ubigia.Api.Transport.Rest;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.Rest;
    using Microsoft.AspNetCore.Mvc;

    [RequiresAuthenticationToken(Role.User)]
    [Route(RelativeDataUri.Properties)]
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
            IActionResult response;
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
            IActionResult response;
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
