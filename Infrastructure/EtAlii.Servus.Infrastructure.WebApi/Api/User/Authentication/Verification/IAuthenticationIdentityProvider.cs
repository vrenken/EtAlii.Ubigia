namespace EtAlii.Servus.Infrastructure
{
    using System.Web.Http.Controllers;

    internal interface IAuthenticationIdentityProvider
    {
        AuthenticationIdentity Get(HttpActionContext actionContext);
    }
}