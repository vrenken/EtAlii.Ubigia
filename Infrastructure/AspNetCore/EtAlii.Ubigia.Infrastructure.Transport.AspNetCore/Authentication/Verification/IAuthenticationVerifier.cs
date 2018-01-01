﻿namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    using System.Net;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public interface IAuthenticationVerifier
    {
        IActionResult Verify(HttpContext context, Controller controller);
    }
}