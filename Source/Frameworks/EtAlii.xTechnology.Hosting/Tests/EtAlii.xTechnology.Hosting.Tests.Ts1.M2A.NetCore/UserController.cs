// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.Api.NetCore
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    [Route("data")]
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Content($"{nameof(UserController)}_{Environment.TickCount}");
        }
        
        [HttpGet("GetComplex")]
        public IActionResult GetComplex(string postfix)
        {
            return Content($"{nameof(UserController)}_{postfix}");
        }
    }
}
