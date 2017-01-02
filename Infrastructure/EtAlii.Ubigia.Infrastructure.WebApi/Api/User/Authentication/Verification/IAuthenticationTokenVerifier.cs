namespace EtAlii.Ubigia.Infrastructure
{
    using System.Net;
    using System.Web.Http.Controllers;

    public interface IAuthenticationTokenVerifier
    {
        HttpStatusCode Verify(HttpActionContext actionContext, string requiredRole);
    }
}