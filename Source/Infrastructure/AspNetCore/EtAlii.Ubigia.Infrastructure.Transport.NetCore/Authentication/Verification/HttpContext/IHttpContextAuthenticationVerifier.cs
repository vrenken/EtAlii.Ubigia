namespace EtAlii.Ubigia.Infrastructure.Transport.NetCore
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public interface IHttpContextAuthenticationVerifier
    {
        IActionResult Verify(HttpContext context, Controller controller, params string[] requiredRoles);
    }
}