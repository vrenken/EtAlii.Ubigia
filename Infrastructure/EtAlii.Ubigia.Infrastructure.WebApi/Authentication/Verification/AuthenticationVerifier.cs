namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Principal;
    using System.Threading;
    using System.Web.Http.Controllers;
    using EtAlii.Ubigia.Infrastructure.Functional;

    internal class AuthenticationVerifier : IAuthenticationVerifier
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IInfrastructureConfiguration _configuration;
        private readonly IAuthenticationIdentityProvider _authenticationIdentityProvider;
        private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

        public AuthenticationVerifier(
            IAccountRepository accountRepository,
            IInfrastructureConfiguration configuration,
            IAuthenticationIdentityProvider authenticationIdentityProvider,
            IAuthenticationTokenConverter authenticationTokenConverter)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
            _authenticationIdentityProvider = authenticationIdentityProvider;
            _authenticationTokenConverter = authenticationTokenConverter;
        }

        public HttpStatusCode Verify(HttpActionContext actionContext, params string[] requiredRoles)
        {
            var identity = _authenticationIdentityProvider.Get(actionContext);
            if (identity == null)
            {
                Challenge(actionContext);
                return HttpStatusCode.Unauthorized;
            }

            var account = _accountRepository.Get(identity.Name, identity.Password);
            if (account == null)
            {
                Challenge(actionContext);
                return HttpStatusCode.Unauthorized;
            }

	        if (requiredRoles.Any())
	        {
		        var hasOneRequiredRole = account.Roles.Any(role => requiredRoles.Any(requiredRole => requiredRole == role));
		        if (!hasOneRequiredRole)
		        {
			        Challenge(actionContext);
			        return HttpStatusCode.Unauthorized;
		        }
			}
			var accountName = account.Name;

            actionContext.Response = CreateResponse(actionContext, accountName);

            var principal = new GenericPrincipal(identity, null);

            Thread.CurrentPrincipal = principal;

            
            //// inside of ASP.NET this is required
            //if (HttpContext.Current != null)
            //{
            //    HttpContext.Current.User = principal;
            //}
            return HttpStatusCode.OK;
        }


        /// <summary>
        /// Send the Authentication Challenge request
        /// </summary>
        /// <param name="message"></param>
        /// <param name="actionContext"></param>
        private void Challenge(HttpActionContext actionContext)
        {
            var host = actionContext.Request.RequestUri.DnsSafeHost;
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

            var respondWithChallenge = true;
            IEnumerable<string> challenges;
            if (actionContext.Request.Headers.TryGetValues("RespondWithChallenge", out challenges))
            {
                respondWithChallenge = challenges.Select(c => c.ToLower()).SingleOrDefault() != "false";
            }

            if (respondWithChallenge)
            {
                actionContext.Response.Headers.Add("WWW-Authenticate", $"Basic realm=\"{host}\"");
            }
        }

        private HttpResponseMessage CreateResponse(HttpActionContext actionContext, string accountName)
        {
            HttpResponseMessage response;
            try
            {
                IEnumerable<string> values;
                var success = actionContext.Request.Headers.TryGetValues("Host-Identifier", out values);
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

                    response = actionContext.Request.CreateResponse(HttpStatusCode.OK, authenticationTokenAsString);
                }
                else
                {
                    response = actionContext.Request.CreateResponse(HttpStatusCode.MethodNotAllowed);
                }
            }
            catch (Exception ex)
            {
                response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                //response = actionContext.Request.CreateResponse<string>(HttpStatusCode.OK, "AllOk");
            }
            return response;

        }
    }
}