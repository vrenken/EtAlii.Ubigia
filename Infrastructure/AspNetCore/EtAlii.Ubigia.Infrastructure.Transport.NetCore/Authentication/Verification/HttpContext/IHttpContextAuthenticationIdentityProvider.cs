namespace EtAlii.Ubigia.Infrastructure.Transport.NetCore
{
    using Microsoft.AspNetCore.Http;

    internal interface IHttpContextAuthenticationIdentityProvider
    {
        AuthenticationIdentity Get(HttpContext context);
    }
}