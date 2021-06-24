// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.RestSystem
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("data")]
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Content($"{typeof(AdminController)}_{Environment.TickCount}");
        }
        
        [HttpGet]
        public IActionResult Get(string postfix)
        {
            return Content($"{typeof(AdminController)}_{postfix}");
        }
    }
}
