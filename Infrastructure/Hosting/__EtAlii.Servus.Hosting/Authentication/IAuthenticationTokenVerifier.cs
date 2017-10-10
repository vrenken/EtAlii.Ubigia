namespace EtAlii.Servus.Infrastructure.Hosting
{
    using System.Net;
    using System.Web.Http.Controllers;

    internal interface IAuthenticationTokenVerifier
    {
        HttpStatusCode Verify(HttpActionContext actionContext);
    }
}