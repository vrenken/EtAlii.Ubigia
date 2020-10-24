namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Admin.Api.NetCore
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    [Route("data")]
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Content($"{nameof(AdminController)}_{Environment.TickCount}");
        }
        
        [HttpGet("GetComplex")]
        public IActionResult GetComplex(string postfix)
        {
            return Content($"{nameof(AdminController)}_{postfix}");
        }
    }
}
