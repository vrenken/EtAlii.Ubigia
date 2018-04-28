namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    using Microsoft.AspNetCore.Http;

    internal interface IHttpContextAuthenticationIdentityProvider
    {
        AuthenticationIdentity Get(HttpContext context);
    }
}