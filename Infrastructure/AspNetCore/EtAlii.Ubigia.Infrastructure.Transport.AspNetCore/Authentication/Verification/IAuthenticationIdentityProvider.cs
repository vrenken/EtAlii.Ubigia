namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    internal interface IAuthenticationIdentityProvider
    {
        AuthenticationIdentity Get(HttpContext context);
    }
}