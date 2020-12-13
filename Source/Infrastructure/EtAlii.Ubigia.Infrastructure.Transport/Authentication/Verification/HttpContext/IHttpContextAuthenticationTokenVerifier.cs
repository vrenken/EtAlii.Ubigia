namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public interface IHttpContextAuthenticationTokenVerifier
    {
        IActionResult Verify(HttpContext context, Controller controller, params string[] requiredRoles);
    }
}