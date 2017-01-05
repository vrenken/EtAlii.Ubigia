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
        private readonly IInfrastructureConfiguration _configuration;
        private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

        public AuthenticationTokenVerifier(
            IAccountRepository accountRepository,
            IInfrastructureConfiguration configuration, 
            IAuthenticationTokenConverter authenticationTokenConverter)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
            _authenticationTokenConverter = authenticationTokenConverter;
        }

        public HttpStatusCode Verify(HttpActionContext actionContext, string requiredRole)
        {
            var result = HttpStatusCode.Forbidden;
            var authenticationToken = _authenticationTokenConverter.FromHttpActionContext(actionContext);
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
                                result = HttpStatusCode.OK;
                            }
                        }
                        else
                        {
                            result = HttpStatusCode.OK;
                        }
                    }
                }
                catch (Exception)
                {
                    result = HttpStatusCode.Forbidden;
                    actionContext.Response = actionContext.Request.CreateResponse<string>(HttpStatusCode.Forbidden, "Unauthorized account");
                }
            }
            else
            {
                result = HttpStatusCode.BadRequest;
                actionContext.Response = actionContext.Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Missing Authentication-Token");
            }

            return result;
        }
    }
}