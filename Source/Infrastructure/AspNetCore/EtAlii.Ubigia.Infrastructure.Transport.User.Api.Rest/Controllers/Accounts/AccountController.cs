namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest
{
	using System;
	using System.Linq;
    using EtAlii.Ubigia.Api.Transport.Rest;
    using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.Ubigia.Infrastructure.Transport.Rest;
	using Microsoft.AspNetCore.Mvc;

	[RequiresAuthenticationToken(Role.User)]
    [Route(RelativeDataUri.Accounts)]
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
		public IActionResult GetForAuthenticationToken([RequiredFromQuery(Name="authenticationToken")] string stringValue)
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
    }
}
