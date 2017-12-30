namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.AspNetCore
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using Microsoft.AspNetCore.Mvc;

    //[RequiresAuthenticationToken]
    [Route(RelativeUri.Data.Spaces)]
    public class SpaceController : WebApiController
    {
        private readonly ISpaceRepository _items;

        //protected ILogger Logger { get { return _logger; } }
        //private readonly ILogger _logger;

        public SpaceController(ISpaceRepository items)
        {
            _items = items;
        }

        // Get all spaces for the specified accountid
        [Route("{accountId}")]
        //public IActionResult GetForAccount([FromUri]Guid accountId)
        public IActionResult GetForAccount(Guid accountId)
        {
            IActionResult response;
            try
            {
                var spaces = _items.GetAll(accountId);
                response = Ok(spaces);
            }
            catch (Exception ex)
            {
                //Logger.Critical("Unable to serve a Space GET client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        [Route("{accountId}/{spaceName}")]
        //public IActionResult GetForAccount([FromUri]Guid accountId, [FromUri]string spaceName)
        public IActionResult GetForAccount(Guid accountId, string spaceName)
        {
            IActionResult response;
            try
            {
                var space = _items.Get(accountId, spaceName);
                response = Ok(space);
            }
            catch (Exception ex)
            {
                //Logger.Critical("Unable to serve a Space GET client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Get all Items
        public IActionResult Get()
        {
            IActionResult response;
            try
            {
                var items = _items.GetAll();
                response = Ok(items);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} GET client request", ex, typeof(T).Name);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Get Item by id
        [Route("{spaceId}")]
        //public IActionResult Get([FromUri]Guid spaceId)
        public IActionResult Get(Guid spaceId)
        {
            IActionResult response;
            try
            {
                var item = _items.Get(spaceId);
                response = Ok(item);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} GET client request", ex, typeof(T).Name);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Add item
        public IActionResult Post([FromBody]Space item, string spaceTemplate)
        {
            IActionResult response;
            try
            {
                var template = SpaceTemplate.All.Single(t => t.Name == spaceTemplate);
                item = _items.Add(item, template);
                response = Ok(item);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} POST client request", ex, typeof(T).Name);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Update Item by id
        [Route("{spaceId}")]
        //public IActionResult Put([FromUri]Guid spaceId, Space space)
        public IActionResult Put(Guid spaceId, Space space)
        {
            IActionResult response;
            try
            {
                var result = _items.Update(spaceId, space);
                response = Ok(result);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} PUT client request", ex, typeof(T).Name);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Delete Item by id
        [Route("{spaceId}")]
        //public IActionResult Delete([FromUri]Guid spaceId)
        public IActionResult Delete(Guid spaceId)
        {
            IActionResult response;
            try
            {
                _items.Remove(spaceId);
                response = Ok();
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} DELETE client request", ex, typeof(T).Name);
                response = BadRequest(ex.Message);
            }
            return response;
        }
    }
}
