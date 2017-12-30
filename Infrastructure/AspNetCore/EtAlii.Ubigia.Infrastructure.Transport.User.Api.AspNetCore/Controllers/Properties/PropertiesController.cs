namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.AspNetCore
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    //[RequiresAuthenticationToken]
    [Authorize]
    public class PropertiesController : WebApiController
    {
        private readonly IPropertiesRepository _properties;

        public PropertiesController(IPropertiesRepository properties)
        {
            _properties = properties;
        }

        [Route(RelativeUri.Data.Properties + "/{entryId}"), HttpGet]
        //public IActionResult Get([FromUri(BinderType = typeof(IdentifierBinder))]Identifier entryId)
        public IActionResult Get([ModelBinder(typeof(IdentifierBinder))]Identifier entryId)
        {
            IActionResult response = null;
            try
            {
                var properties = _properties.Get(entryId);
                response = Ok(properties);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a properties GET client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        /// <summary>
        /// Post a new contentdefinition for the specified content.
        /// </summary>
        /// <param name="entryId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [Route(RelativeUri.Data.Properties + "/{entryId}"), HttpPost]
        //public IActionResult Post([FromUri(BinderType = typeof(IdentifierBinder))]Identifier entryId, [FromBody]PropertyDictionary properties)
        public IActionResult Post([ModelBinder(typeof(IdentifierBinder))]Identifier entryId, [FromBody]PropertyDictionary properties)
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
                //_logger.Critical("Unable to serve a Content POST client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }
    }
}
