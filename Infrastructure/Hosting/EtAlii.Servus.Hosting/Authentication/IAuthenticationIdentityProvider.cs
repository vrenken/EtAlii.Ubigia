namespace EtAlii.Servus.Infrastructure.Hosting
{
    using System.Web.Http.Controllers;

    internal interface IAuthenticationIdentityProvider
    {
        AuthenticationIdentity Get(HttpActionContext actionContext);
    }
}