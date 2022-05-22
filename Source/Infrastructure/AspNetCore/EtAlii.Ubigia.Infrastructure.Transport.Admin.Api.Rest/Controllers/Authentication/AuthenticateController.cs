// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest
{
    using System.Threading.Tasks;
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
        public async Task<IActionResult> Get()
        {
            var result = await _authenticationVerifier
                .Verify(HttpContext, this, Role.Admin, Role.System)
                .ConfigureAwait(false);
            return result;
        }
    }
}
