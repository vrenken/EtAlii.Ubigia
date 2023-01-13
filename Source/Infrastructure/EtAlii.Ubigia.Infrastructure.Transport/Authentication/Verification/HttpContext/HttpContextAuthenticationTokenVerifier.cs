// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport;

using System;
using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Functional;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

internal class HttpContextAuthenticationTokenVerifier : IHttpContextAuthenticationTokenVerifier
{
    private readonly IAccountRepository _accountRepository;
    private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

    public HttpContextAuthenticationTokenVerifier(
        IAccountRepository accountRepository,
        IAuthenticationTokenConverter authenticationTokenConverter)
    {
        _accountRepository = accountRepository;
        _authenticationTokenConverter = authenticationTokenConverter;
    }

    /// <inheritdoc />
    public async Task<IActionResult> Verify(HttpContext context, Controller controller, params string[] requiredRoles)
    {
        IActionResult result;
        var authenticationToken = _authenticationTokenConverter.FromHttpActionContext(context);
        if (authenticationToken != null)
        {
            result = controller.Forbid();
            try
            {
                var account = await _accountRepository.Get(authenticationToken.Name).ConfigureAwait(false);
                if (account != null)
                {
                    // Let's be a bit safe, if the requiredRole is not null we are going to check the roles collection for it.
                    if (requiredRoles.Any())
                    {
                        var hasOneRequiredRole = account.Roles.Any(role => requiredRoles.Any(requiredRole => requiredRole == role));
                        if (hasOneRequiredRole)
                        {
                            result = controller.Ok();
                        }
                    }
                    else
                    {
                        result = controller.Ok();
                    }
                }
            }
            catch (Exception)
            {
                result = controller.Forbid("Unauthorized account");
            }
        }
        else
        {
            result = controller.BadRequest("Missing Authentication-Token");
        }

        return result;
    }
}
