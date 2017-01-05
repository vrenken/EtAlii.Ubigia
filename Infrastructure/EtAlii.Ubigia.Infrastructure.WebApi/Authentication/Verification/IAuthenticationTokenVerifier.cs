namespace EtAlii.Ubigia.Infrastructure.Transport.WebApi
{
    using System.Net;
    using System.Web.Http.Controllers;

    public interface IAuthenticationTokenVerifier
    {
        HttpStatusCode Verify(HttpActionContext actionContext, string requiredRole);
    }
}