namespace EtAlii.Servus.Infrastructure
{
    using System;
    using System.Text;
    using System.Web.Http.Controllers;

    internal class DefaultAuthenticationIdentityProvider : IAuthenticationIdentityProvider
    {
        /// <summary>
        /// Parses the Authorization header and creates account credentials
        /// </summary>
        /// <param name="actionContext"></param>
        public AuthenticationIdentity Get(HttpActionContext actionContext)
        {
            string authHeader = null;
            var auth = actionContext.Request.Headers.Authorization;
            if (auth != null && auth.Scheme == "Basic")
            {
                authHeader = auth.Parameter;
            }

            if (string.IsNullOrEmpty(authHeader))
            {
                return null;
            }
            authHeader = Encoding.Default.GetString(Convert.FromBase64String(authHeader));

            var tokens = authHeader.Split(':');
            if (tokens.Length < 2)
            {
                return null;
            }
            return new AuthenticationIdentity(tokens[0], tokens[1]);
        }
    }
}