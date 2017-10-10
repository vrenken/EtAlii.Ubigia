namespace EtAlii.Servus.Infrastructure.Hosting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Principal;
    using System.Threading;
    using System.Web.Http.Controllers;
    using EtAlii.Servus.Api;

    internal class AuthenticationVerifier : IAuthenticationVerifier
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IInfrastructureConfiguration _configuration;
        private readonly IDiagnosticsConfiguration _diagnostics;
        private readonly IAuthenticationIdentityProvider _authenticationIdentityProvider;

        public AuthenticationVerifier(
            IAccountRepository accountRepository,
            IInfrastructureConfiguration configuration,
            IDiagnosticsConfiguration diagnostics, IAuthenticationIdentityProvider authenticationIdentityProvider)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
            _diagnostics = diagnostics;
            _authenticationIdentityProvider = authenticationIdentityProvider;
        }

        public HttpStatusCode Verify(HttpActionContext actionContext)
        {
            var identity = _authenticationIdentityProvider.Get(actionContext);
            if (identity == null)
            {
                Challenge(actionContext);
                return HttpStatusCode.Unauthorized;
            }

            var isAdmin = _configuration.Account == identity.Name && _configuration.Password == identity.Password;

            var account = _accountRepository.Get(identity.Name, identity.Password);
            if (account == null && !isAdmin)
            {
                Challenge(actionContext);
                return HttpStatusCode.Unauthorized;
            }
            var accountName = isAdmin ? _configuration.Account : account.Name;

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
            actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", host));
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

                    var authenticationTokenAsBytes = AuthenticationTokenConverter.ToBytes(authenticationToken);
                    authenticationTokenAsBytes = Aes.Encrypt(authenticationTokenAsBytes);
                    var authenticationTokenAsString = Convert.ToBase64String(authenticationTokenAsBytes);

                    response = actionContext.Request.CreateResponse<string>(HttpStatusCode.OK, authenticationTokenAsString);
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