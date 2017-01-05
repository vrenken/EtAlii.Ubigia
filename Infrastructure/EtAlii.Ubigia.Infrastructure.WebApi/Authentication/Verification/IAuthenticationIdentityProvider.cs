namespace EtAlii.Ubigia.Infrastructure.Transport.WebApi
{
    using System.Web.Http.Controllers;

    internal interface IAuthenticationIdentityProvider
    {
        AuthenticationIdentity Get(HttpActionContext actionContext);
    }
}