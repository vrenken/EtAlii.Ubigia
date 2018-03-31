namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest.AspNetCore
{
	using EtAlii.Ubigia.Api.Transport;
	using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
	using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

	[Route(RelativeUri.Authenticate)]
    public class AuthenticateController : RestController
	{
        private readonly IHttpContextAuthenticationVerifier _authenticationVerifier;

        public AuthenticateController(IHttpContextAuthenticationVerifier authenticationVerifier)
        {
            _authenticationVerifier = authenticationVerifier;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var result = _authenticationVerifier.Verify(HttpContext, this, Role.User, Role.System);
            return result;
        }
    }
}
