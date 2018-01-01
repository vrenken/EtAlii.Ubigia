namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    using System.Net;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public interface IAuthenticationTokenVerifier
    {
        IActionResult Verify(HttpContext actionContext, Controller controller, string requiredRole);
    }
}