namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Rest;
    using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.Ubigia.Infrastructure.Transport.Rest;
	using Microsoft.AspNetCore.Mvc;

	[RequiresAuthenticationToken(Role.Admin)]
    [Route(RelativeManagementUri.Accounts)]
    public class AccountController : RestController
    {
	    private readonly IAccountRepository _items;
	    private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

	    public AccountController(
		    IAccountRepository items,
		    IAuthenticationTokenConverter authenticationTokenConverter)
	    {
		    _items = items;
		    _authenticationTokenConverter = authenticationTokenConverter;
	    }

		[HttpGet]
		public IActionResult GetForAuthenticationToken([RequiredFromQuery(Name = "authenticationToken")] string stringValue)
	    {
			IActionResult response;
		    try
		    {
			    HttpContext.Request.Headers.TryGetValue("Authentication-Token", out var stringValues);
			    var authenticationTokenAsString = stringValues.Single();
			    var authenticationToken = _authenticationTokenConverter.FromString(authenticationTokenAsString);

			    var account = _items.Get(authenticationToken.Name);
			    response = Ok(account);
		    }
		    catch (Exception ex)
		    {
			    //Logger.Critical("Unable to serve a Account GET client request", ex)
			    response = BadRequest(ex.Message);
		    }
		    return response;
	    }

		[HttpGet]
		public IActionResult GetByName([RequiredFromQuery]string accountName)
		{
			IActionResult response;
			try
			{
				var account = _items.Get(accountName);
				response = Ok(account);
			}
			catch (Exception ex)
			{
				//Logger.Critical("Unable to serve a Account GET client request", ex)
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
                //Logger.Warning("Unable to serve a [0] GET client request", ex, typeof(T).Name)
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Get Item by id
        [HttpGet]
        public IActionResult Get([RequiredFromQuery]Guid accountId)
        {
            IActionResult response;
            try
            {
                var item = _items.Get(accountId);
                response = Ok(item);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a [0] GET client request", ex, typeof(T).Name)
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Add item
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Account item, [RequiredFromQuery]string accountTemplate)
        {
            IActionResult response;
            try
            {
                var template = AccountTemplate.All.Single(t => t.Name == accountTemplate);
                item = await _items.Add(item, template).ConfigureAwait(false);
                response = Ok(item);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a [0] POST client request", ex, typeof(T).Name)
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Update Item by id
        [HttpPut]
        public IActionResult Put([FromBody]Account account, [RequiredFromQuery]Guid accountId)
        {
            IActionResult response;
            try
            {
                var result = _items.Update(accountId, account);
                response = Ok(result);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a [0] PUT client request", ex, typeof(T).Name)
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Delete Item by id
        [HttpDelete]
        public IActionResult Delete([RequiredFromQuery]Guid accountId)
        {
            IActionResult response;
            try
            {
                _items.Remove(accountId);
                response = Ok();
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a [0] DELETE client request", ex, typeof(T).Name)
                response = BadRequest(ex.Message);
            }
            return response;
        }
    }
}
