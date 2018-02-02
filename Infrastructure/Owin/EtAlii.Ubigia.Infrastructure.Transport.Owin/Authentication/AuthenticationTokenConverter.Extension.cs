namespace EtAlii.Ubigia.Infrastructure.Transport.Owin
{
    using System.Linq;
    using System.Web.Http.Controllers;

    public static class AuthenticationTokenConverterExtension
    {
        public static AuthenticationToken FromHttpActionContext(this IAuthenticationTokenConverter converter, HttpActionContext actionContext)
        {
            var authenticationTokenAsString = actionContext.Request.Headers
                .GetValues("Authentication-Token")
                .FirstOrDefault();
            return authenticationTokenAsString != null ? converter.FromString(authenticationTokenAsString) : null;
        }
    }
}