namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest
{
    using EtAlii.Ubigia.Api.Transport.Rest;
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
            var result = _authenticationVerifier.Verify(HttpContext, this, Role.Admin, Role.System);
            return result;
        }
    }
}
