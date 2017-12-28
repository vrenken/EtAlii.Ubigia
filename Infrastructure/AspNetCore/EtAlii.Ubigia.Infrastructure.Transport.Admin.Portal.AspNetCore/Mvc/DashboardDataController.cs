namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.AspNetCore
{
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;


    [Route("[controller]")]
    public class DashboardDataController : AdminPortalController
    {
        //private IHiService _adminService;

        //public AdminDataController(IHiService adminService)
        //{
        //    _adminService = adminService;
        //}

        [HttpGet]
        public string Get()
        {
            return "I'm Admin Data Controller. ";// + _adminService.SayHi();
        }
    }
}
