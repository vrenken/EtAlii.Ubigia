namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    [Route("")]
    public class AuthenticateController : Controller
    {
        private readonly IAuthenticationVerifier _authenticationVerifier;

        public AuthenticateController(IAuthenticationVerifier authenticationVerifier)
        {
            _authenticationVerifier = authenticationVerifier;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var result = _authenticationVerifier.Verify(HttpContext, this);
            return result;
        }
    }
}
