﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public interface IHttpContextAuthenticationVerifier
{
    Task<IActionResult> Verify(HttpContext context, Controller controller, params string[] requiredRoles);
}
