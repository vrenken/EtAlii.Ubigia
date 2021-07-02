// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.Razor
{
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    public class HomeController : AdminPortalController
    {
        //private IHiService _adminService

        //public AdminDataController(IHiService adminService)
        //[
        //    _adminService = adminService
        //]
        [HttpGet]
        public string Get()
        {
#pragma warning disable S3400
            // This method+string cannot be converted into a constant as it controller method.
            return "This is my default action...";
#pragma warning restore S3400
        }
    }
}
