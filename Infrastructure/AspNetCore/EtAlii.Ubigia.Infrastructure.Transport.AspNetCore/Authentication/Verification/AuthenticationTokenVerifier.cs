namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    using System;
    using System.Linq;
    using System.Net;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    internal class AuthenticationTokenVerifier : IAuthenticationTokenVerifier
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

        public AuthenticationTokenVerifier(
            IAccountRepository accountRepository,
            IAuthenticationTokenConverter authenticationTokenConverter)
        {
            _accountRepository = accountRepository;
            _authenticationTokenConverter = authenticationTokenConverter;
        }

        public IActionResult Verify(HttpContext context, Controller controller, string requiredRole)
        {
            IActionResult result = controller.Forbid();
            var authenticationToken = _authenticationTokenConverter.FromHttpActionContext(context);
            if (authenticationToken != null)
            {
                try
                {
                    var account = _accountRepository.Get(authenticationToken.Name);
                    if (account != null)
                    {
                        // Let's be a bit safe, if the requiredRole is not null we are going to check the roles collection for it.
                        if (requiredRole != null)
                        {
                            if (account.Roles.Contains(requiredRole))
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
}