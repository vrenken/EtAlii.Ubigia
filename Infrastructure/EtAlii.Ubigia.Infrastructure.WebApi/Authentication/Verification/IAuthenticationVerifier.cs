namespace EtAlii.Ubigia.Infrastructure.Transport.WebApi
{
    using System.Net;
    using System.Web.Http.Controllers;

    public interface IAuthenticationVerifier
    {
        HttpStatusCode Verify(HttpActionContext actionContext);
    }
}