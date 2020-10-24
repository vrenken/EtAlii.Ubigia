namespace EtAlii.xTechnology.Hosting.Tests.RestSystem
{
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
}
