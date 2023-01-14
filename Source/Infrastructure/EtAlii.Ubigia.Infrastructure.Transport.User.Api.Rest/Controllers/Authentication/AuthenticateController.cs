// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest;

using System;
using System.Threading.Tasks;
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
    public async Task<IActionResult> Get()
    {
        var result = await _authenticationVerifier
            .Verify(HttpContext, this, Role.User, Role.System)
            .ConfigureAwait(false);
        return result;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get([RequiredFromQuery]string accountName, [RequiredFromQuery(Name = "authenticationToken")] string value)
    {
        IActionResult response;
        try
        {
            response = await _authenticationTokenVerifier
                .Verify(HttpContext, this, Role.Admin, Role.System)
                .ConfigureAwait(false);
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
