namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using EtAlii.Ubigia.Infrastructure.Functional;

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

        public HttpStatusCode Verify(HttpActionContext actionContext, params string[] requiredRoles)
        {
            var authenticationToken = _authenticationTokenConverter.FromHttpActionContext(actionContext);
	        if (authenticationToken == null)
	        {
		        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, "Missing Authentication-Token");
		        return HttpStatusCode.BadRequest;
	        }

			try
            {
                var account = _accountRepository.Get(authenticationToken.Name);
	            if (account == null)
	            {
		            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "Unauthorized account");
		            return HttpStatusCode.Forbidden;
				}

				// Let's be a bit safe, if the requiredRole is not null we are going to check the roles collection for it.
				if (requiredRoles.Any())
                {
	                var hasOneRequiredRole = account.Roles.Any(role => requiredRoles.Any(requiredRole => requiredRole == role));
	                if (!hasOneRequiredRole)
	                {
		                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "Unauthorized account: Account does not contain the required role");
		                return HttpStatusCode.Forbidden;
	                }
				}
            }
            catch (Exception)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "Unauthorized account");
	            return HttpStatusCode.Forbidden;
            }

			return HttpStatusCode.OK;
        }
	}
}