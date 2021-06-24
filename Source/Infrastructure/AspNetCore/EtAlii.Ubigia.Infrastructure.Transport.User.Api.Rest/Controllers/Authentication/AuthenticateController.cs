// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest
{
	using System;
    using EtAlii.Ubigia.Api.Transport.Rest;
    using EtAlii.Ubigia.Infrastructure.Transport.Rest;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Route(RelativeUri.Authenticate)]
    public class AuthenticateController : RestController
	{
		private readonly IHttpContextAuthenticationVerifier _authenticationVerifier;
		private readonly IHttpContextResponseBuilder _responseBuilder;
		private readonly IHttpContextAuthenticationTokenVerifier _authenticationTokenVerifier;

		public AuthenticateController(
			IHttpContextAuthenticationVerifier authenticationVerifier,
			IHttpContextResponseBuilder responseBuilder,
			IHttpContextAuthenticationTokenVerifier authenticationTokenVerifier)
		{
			_authenticationVerifier = authenticationVerifier;
			_responseBuilder = responseBuilder;
			_authenticationTokenVerifier = authenticationTokenVerifier;
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
			IActionResult response;
			try
			{
				response = _authenticationTokenVerifier.Verify(HttpContext, this, Role.Admin, Role.System);
				if (response is OkResult)
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
