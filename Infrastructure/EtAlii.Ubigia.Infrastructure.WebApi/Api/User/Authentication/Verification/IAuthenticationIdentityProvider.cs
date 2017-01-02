namespace EtAlii.Ubigia.Infrastructure
{
    using System.Web.Http.Controllers;

    internal interface IAuthenticationIdentityProvider
    {
        AuthenticationIdentity Get(HttpActionContext actionContext);
    }
}