namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest.AspNetCore
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using Microsoft.AspNetCore.Mvc;

    //[RequiresAuthenticationToken]
    //[Authorize]
    [Route(RelativeUri.Admin.Api.Spaces)]
    public class SpaceController : RestController
    {
	    private readonly ISpaceRepository _items;

	    public SpaceController(ISpaceRepository items)
	    {
		    _items = items;
	    }

		// Get all spaces for the specified accountid
		[HttpGet]
        public IActionResult GetForAccount([RequiredFromQuery]Guid accountId)
        {
            IActionResult response;
            try
            {
                var spaces = _items.GetAll(accountId);
                response = Ok(spaces);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }

		[HttpGet]
		public IActionResult GetForAccount([RequiredFromQuery]Guid accountId, [RequiredFromQuery]string spaceName)
		{
			IActionResult response;
			try
			{
				var space = _items.Get(accountId, spaceName);
				response = Ok(space);
			}
			catch (Exception ex)
			{
				response = BadRequest(ex.Message);
			}
			return response;
		}

		// Get all Items
		[HttpGet]
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
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Get Item by id
        [HttpGet]
        public IActionResult Get([RequiredFromQuery]Guid spaceId)
        {
            IActionResult response;
            try
            {
                var item = _items.Get(spaceId);
                response = Ok(item);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Add item
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Space item, string spaceTemplate)
        {
            IActionResult response;
            try
            {
                var template = SpaceTemplate.All.Single(t => t.Name == spaceTemplate);
                item = await _items.Add(item, template);
                response = Ok(item);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Update Item by id
        [HttpPut]
        public IActionResult Put([RequiredFromQuery]Guid spaceId, [FromBody]Space space)
        {
            IActionResult response;
            try
            {
                var result = _items.Update(spaceId, space);
                response = Ok(result);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Delete Item by id
        [HttpDelete]
        public IActionResult Delete([RequiredFromQuery]Guid spaceId)
        {
            IActionResult response;
            try
            {
                _items.Remove(spaceId);
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
