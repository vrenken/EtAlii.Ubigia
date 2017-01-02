namespace EtAlii.Servus.Infrastructure.Hosting
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using EtAlii.Servus.Api;

    internal class AuthenticationTokenVerifier : IAuthenticationTokenVerifier
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IInfrastructureConfiguration _configuration;
        private readonly IDiagnosticsConfiguration _diagnostics;

        public AuthenticationTokenVerifier(
            IAccountRepository accountRepository,
            IInfrastructureConfiguration configuration,
            IDiagnosticsConfiguration diagnostics)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
            _diagnostics = diagnostics;
        }

        public HttpStatusCode Verify(HttpActionContext actionContext)
        {
            var result = HttpStatusCode.Forbidden;
            var authenticationTokenAsString = GetAuthenticationTokenStringFromHeaders(actionContext);
            if (!String.IsNullOrWhiteSpace(authenticationTokenAsString))
            {
                try
                {
                    var authenticationToken = CreateAuthenticationToken(authenticationTokenAsString);
                    var account = _accountRepository.Get(authenticationToken.Name);
                    if (account != null)
                    {
                        result = HttpStatusCode.OK;
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


        private AuthenticationToken CreateAuthenticationToken(string authenticationTokenAsString)
        {
            var authenticationTokenAsBytes = Convert.FromBase64String(authenticationTokenAsString);
            authenticationTokenAsBytes = Aes.Decrypt(authenticationTokenAsBytes);
            var authenticationToken = AuthenticationTokenConverter.FromBytes(authenticationTokenAsBytes);
            return authenticationToken;
        }

        private string GetAuthenticationTokenStringFromHeaders(HttpActionContext actionContext)
        {
            var authenticationTokenAsString = actionContext.Request.Headers.GetValues("Authentication-Token")
                                                                           .FirstOrDefault();
            return authenticationTokenAsString;
        }
    }
}