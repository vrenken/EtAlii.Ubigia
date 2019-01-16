namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest.AspNetCore
{
	using System;
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Primitives;

	//[RequiresAuthenticationToken]
    [Route(RelativeUri.User.Api.Spaces)]
    public class SpaceController : RestController
    {
	    private readonly ISpaceRepository _items;
	    private readonly IAccountRepository _accountItems;
	    private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

		public SpaceController(
			ISpaceRepository items, 
			IAccountRepository accountItems, 
			IAuthenticationTokenConverter authenticationTokenConverter)
		{
			_items = items;
			_accountItems = accountItems;
			_authenticationTokenConverter = authenticationTokenConverter;
		}

        // Get all spaces for the specified accountid
        [HttpGet]
	    public IActionResult GetForAuthenticationToken([RequiredFromQuery(Name = "authenticationToken")] string stringValue, [RequiredFromQuery]string spaceName)
		{
            IActionResult response;
            try
            {
	            HttpContext.Request.Headers.TryGetValue("Authentication-Token", out StringValues stringValues);
	            var authenticationTokenAsString = stringValues.Single();
	            var authenticationToken = _authenticationTokenConverter.FromString(authenticationTokenAsString);

	            var account = _accountItems.Get(authenticationToken.Name);

	            var space = _items.Get(account.Id, spaceName);

	            response = Ok(space);
            }
			catch (Exception ex)
            {
                //Logger.Critical("Unable to serve a Space GET client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }
    }
}
