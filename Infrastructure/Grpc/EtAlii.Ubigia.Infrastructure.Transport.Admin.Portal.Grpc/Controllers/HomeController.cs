﻿namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.Grpc
{
    using Microsoft.Grpc.Mvc;

    [Route("[controller]")]
    public class HomeController : AdminPortalController
    {
        //private IHiService _adminService;

        //public AdminDataController(IHiService adminService)
        //{
        //    _adminService = adminService;
        //}

        [HttpGet]
        public string Get()
        {
            return "This is my default action...";
        }
    }
}
