namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public interface IHttpContextAuthenticationTokenVerifier
    {
        IActionResult Verify(HttpContext actionContext, Controller controller, params string[] requiredRoles);
    }
}