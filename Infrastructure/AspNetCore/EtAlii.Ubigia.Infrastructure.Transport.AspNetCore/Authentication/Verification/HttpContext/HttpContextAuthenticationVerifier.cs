namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    using System;
    using System.Linq;
    using System.Security.Principal;
    using System.Threading;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.ApplicationInsights.AspNetCore.Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Primitives;

    internal class HttpContextAuthenticationVerifier : IHttpContextAuthenticationVerifier
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAuthenticationIdentityProvider _authenticationIdentityProvider;
        private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

        public HttpContextAuthenticationVerifier(
            IAccountRepository accountRepository,
            IHttpContextAuthenticationIdentityProvider authenticationIdentityProvider,
            IAuthenticationTokenConverter authenticationTokenConverter)
        {
            _accountRepository = accountRepository;
            _authenticationIdentityProvider = authenticationIdentityProvider;
            _authenticationTokenConverter = authenticationTokenConverter;
        }

        public IActionResult Verify(HttpContext context, Controller controller, params string[] requiredRoles)
        {
            var identity = _authenticationIdentityProvider.Get(context);
            if (identity == null)
            {
                return Challenge(context, controller);
            }

            var account = _accountRepository.Get(identity.Name, identity.Password);
            if (account == null)
            {
                return Challenge(context, controller);
            }
	        if (requiredRoles.Any())
	        {
		        var hasOneRequiredRole = account.Roles.Any(role => requiredRoles.Any(requiredRole => requiredRole == role));
		        if (!hasOneRequiredRole)
		        {
			        throw new UnauthorizedInfrastructureOperationException("Invalid role");
		        }
	        }

			var accountName = account.Name;

            var response = CreateResponse(context, controller, accountName);

            var principal = new GenericPrincipal(identity, null);

            Thread.CurrentPrincipal = principal;


            //// inside of ASP.NET this is required
            //if (HttpContext.Current != null)
            //{
            //    HttpContext.Current.User = principal;
            //}
            return response;
        }


        /// <summary>
        /// Send the Authentication Challenge request
        /// </summary>
        /// <param name="context"></param>
        /// <param name="controller"></param>
        private IActionResult Challenge(HttpContext context, Controller controller)
        {
            //var host = context.Request.RequestUri.DnsSafeHost;
            var host = context.Request.GetUri().DnsSafeHost;

            var respondWithChallenge = true;
            if (context.Request.Headers.TryGetValue("RespondWithChallenge", out StringValues challenges))
            {
                respondWithChallenge = challenges.Select(c => c.ToLower()).SingleOrDefault() != "false";
            }

            if (respondWithChallenge)
            {
                context.Response.Headers.Add("WWW-Authenticate", $"Basic realm=\"{host}\"");
            }
            return controller.Unauthorized();
        }

        private IActionResult CreateResponse(HttpContext context, Controller controller, string accountName)
        {
            IActionResult response;
            try
            {
                var success = context.Request.Headers.TryGetValue("Host-Identifier", out StringValues values);
                if (success)
                {
                    var hostIdentifier = values.First();

                    var authenticationToken = new AuthenticationToken
                    {
                        Name = accountName,
                        Address = hostIdentifier,
                        Salt = DateTime.UtcNow.ToBinary(),
                    };

                    var authenticationTokenAsBytes = _authenticationTokenConverter.ToBytes(authenticationToken);
                    authenticationTokenAsBytes = Aes.Encrypt(authenticationTokenAsBytes);
                    var authenticationTokenAsString = Convert.ToBase64String(authenticationTokenAsBytes);

                    response = controller.Ok(authenticationTokenAsString);
                }
                else
                {
                    response = new StatusCodeResult(405); //HttpStatusCode.MethodNotAllowed;
                }
            }
            catch (Exception ex)
            {
                response = controller.BadRequest(ex.Message);
                //response = actionContext.Request.CreateResponse<string>(HttpStatusCode.OK, "AllOk");
            }
            return response;

        }
    }
}