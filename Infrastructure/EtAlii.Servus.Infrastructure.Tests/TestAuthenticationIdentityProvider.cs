namespace EtAlii.Servus.Infrastructure.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Controllers;
    using EtAlii.Servus.Infrastructure.WebApi;

    internal class TestAuthenticationIdentityProvider : IAuthenticationIdentityProvider
    {
        public AuthenticationIdentity Get(HttpActionContext actionContext)
        {
            var userName = String.Empty;
            var password = String.Empty;
            IEnumerable<string> values;
            if (actionContext.Request.Headers.TryGetValues("Test-UserName", out values))
            {
                userName = values.SingleOrDefault();
            }
            if (actionContext.Request.Headers.TryGetValues("Test-Password", out values))
            {
                password = values.SingleOrDefault();
            }

            var hasCredentials = !String.IsNullOrWhiteSpace(password) && !String.IsNullOrWhiteSpace(userName);
            return hasCredentials ? new AuthenticationIdentity(userName, password) : null;
        }
    }
}