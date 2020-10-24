namespace EtAlii.Ubigia.Infrastructure.Transport.NetCore
{
    using System.Linq;
    using Microsoft.AspNetCore.Http;

    public static class AuthenticationTokenConverterExtension
    {
        public static AuthenticationToken FromHttpActionContext(this IAuthenticationTokenConverter converter, HttpContext actionContext)
        {

            actionContext.Request.Headers.TryGetValue("Authentication-Token", out var values);
            var authenticationTokenAsString = values.FirstOrDefault();
            return authenticationTokenAsString != null ? converter.FromString(authenticationTokenAsString) : null;
        }
    }
}