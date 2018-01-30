﻿namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi
{
    using System.Net;
    using System.Web.Http.Controllers;

    public interface IAuthenticationTokenVerifier
    {
        HttpStatusCode Verify(HttpActionContext actionContext, params string[] requiredRoles);
    }
}