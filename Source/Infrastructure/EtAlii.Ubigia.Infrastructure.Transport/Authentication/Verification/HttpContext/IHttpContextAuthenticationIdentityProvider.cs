namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using Microsoft.AspNetCore.Http;

    internal interface IHttpContextAuthenticationIdentityProvider
    {
        AuthenticationIdentity Get(HttpContext context);
    }
}