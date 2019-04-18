namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    using System;
    using System.Linq;
    using System.Text;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;

    internal class DefaultHttpContextAuthenticationIdentityProvider : IHttpContextAuthenticationIdentityProvider
    {
        /// <summary>
        /// Parses the Authorization header and creates account credentials
        /// </summary>
        /// <param name="context"></param>
        public AuthenticationIdentity Get(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("Authorization", out StringValues values))
            {
                var authenticationHeader = values.FirstOrDefault();
                if (String.IsNullOrWhiteSpace(authenticationHeader))
                {
                    return null;
                }

                if (!authenticationHeader.StartsWith("Basic", StringComparison.OrdinalIgnoreCase))
                {
                    return null;
                }

                // Based on reply in: https://stackoverflow.com/questions/38977088/asp-net-core-web-api-authentication
                var encodedUsernamePassword = authenticationHeader.Substring("Basic".Length).Trim();
                var usernamePassword = Encoding.Default.GetString(Convert.FromBase64String(encodedUsernamePassword));

                var separatorIndex = usernamePassword.IndexOf(':');
                if (separatorIndex == -1)
                {
                    return null;
                }
                var username = usernamePassword.Substring(0, separatorIndex);
                var password = usernamePassword.Substring(separatorIndex + 1);

                return new AuthenticationIdentity(username, password);
            }
            return null;
        }
        //public AuthenticationIdentity Get(HttpContext context)
        //{
        //    string authHeader = null
        //    var auth = context.Request.Headers.Authorization
        //    if (auth != null && auth.Scheme == "Basic")
        //    {
        //        authHeader = auth.Parameter
        //    }

        //    if (string.IsNullOrEmpty(authHeader))
        //    {
        //        return null
        //    }
        //    authHeader = Encoding.Default.GetString(Convert.FromBase64String(authHeader))

        //    var tokens = authHeader.Split(':')
        //    if (tokens.Length < 2)
        //    {
        //        return null
        //    }
        //    return new AuthenticationIdentity(tokens[0], tokens[1])
        //}
    }
}