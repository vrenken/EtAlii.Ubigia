namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest.AspNetCore
{
	using System;
	using System.Linq;
	using EtAlii.Ubigia.Api.Transport;
	using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
	using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Primitives;

	[Route(RelativeUri.Authenticate)]
    public class AuthenticateController : RestController
	{
		private readonly IAccountRepository _items;
		private readonly IHttpContextAuthenticationVerifier _authenticationVerifier;
		private readonly IAuthenticationTokenConverter _authenticationTokenConverter;
		private readonly IHttpContextResponseBuilder _responseBuilder;

		public AuthenticateController(
			IAccountRepository items,
			IHttpContextAuthenticationVerifier authenticationVerifier, 
			IAuthenticationTokenConverter authenticationTokenConverter, 
			IHttpContextResponseBuilder responseBuilder)
		{
			_items = items;
			_authenticationVerifier = authenticationVerifier;
			_authenticationTokenConverter = authenticationTokenConverter;
			_responseBuilder = responseBuilder;
		}

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var result = _authenticationVerifier.Verify(HttpContext, this, Role.User, Role.System);
            return result;
        }

		[AllowAnonymous]
		[HttpGet]
		public IActionResult Get([RequiredFromQuery]string accountName, [RequiredFromQuery(Name = "authenticationToken")] string value)
		{
			IActionResult response = null;
			try
			{
				HttpContext.Request.Headers.TryGetValue("Authentication-Token", out StringValues stringValues);
				var authenticationTokenAsString = stringValues.Single();
				var authenticationToken = _authenticationTokenConverter.FromString(authenticationTokenAsString);

				var account = _items.Get(authenticationToken.Name);
				if (account.Roles.Any(role => role == Role.Admin || role == Role.System))
				{
					response = _responseBuilder.Build(HttpContext, this, accountName);
				}
			}
			catch (Exception ex)
			{
				response = BadRequest(ex.Message);
			}
			return response;
		}
	}
}
