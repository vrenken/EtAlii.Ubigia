namespace EtAlii.Servus.Infrastructure
{
    using System.Net;
    using System.Web.Http.Controllers;

    public interface IAuthenticationVerifier
    {
        HttpStatusCode Verify(HttpActionContext actionContext);
    }
}