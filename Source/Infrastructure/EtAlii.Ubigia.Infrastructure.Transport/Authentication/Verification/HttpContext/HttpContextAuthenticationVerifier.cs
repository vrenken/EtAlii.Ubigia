// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Principal;
    using System.Threading;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Mvc;

    internal class HttpContextAuthenticationVerifier : IHttpContextAuthenticationVerifier
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAuthenticationIdentityProvider _authenticationIdentityProvider;
	    private readonly IHttpContextResponseBuilder _responseBuilder;

		public HttpContextAuthenticationVerifier(
            IAccountRepository accountRepository,
            IHttpContextAuthenticationIdentityProvider authenticationIdentityProvider,
            IHttpContextResponseBuilder responseBuilder)
        {
            _accountRepository = accountRepository;
            _authenticationIdentityProvider = authenticationIdentityProvider;
	        _responseBuilder = responseBuilder;
        }

        [SuppressMessage(
            category: "Sonar Code Smell",
            checkId: "S4834:Controlling permissions is security-sensitive",
            Justification = "Safe to do so here.")]
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

            var response = _responseBuilder.Build(context, controller, accountName);

            var principal = new GenericPrincipal(identity, null);

            Thread.CurrentPrincipal = principal;


            //// inside of ASP.NET this is required
            //if [HttpContext.Current != null]
            //[
            //    HttpContext.Current.User = principal
            //]
            return response;
        }


        /// <summary>
        /// Send the Authentication Challenge request
        /// </summary>
        /// <param name="context"></param>
        /// <param name="controller"></param>
        private IActionResult Challenge(HttpContext context, Controller controller)
        {
            //var host = context.Request.RequestUri.DnsSafeHost
            //var host = context.Request.GetUri().DnsSafeHost
            var host = new Uri(context.Request.GetDisplayUrl()).DnsSafeHost;

            var respondWithChallenge = true;
            if (context.Request.Headers.TryGetValue("RespondWithChallenge", out var challenges))
            {
                respondWithChallenge = challenges.Select(c => c.ToLower()).SingleOrDefault() != "false";
            }

            if (respondWithChallenge)
            {
                context.Response.Headers.Add("WWW-Authenticate", $"Basic realm=\"{host}\"");
            }
            return controller.Unauthorized();
        }
    }
}
