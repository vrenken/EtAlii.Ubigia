// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.RestSystem;

using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("data")]
public class UserController : Controller
{
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Get()
    {
        return Content($"{typeof(UserController)}_{Environment.TickCount}");
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Get(string postfix)
    {
        return Content($"{typeof(UserController)}_{postfix}");
    }
}
