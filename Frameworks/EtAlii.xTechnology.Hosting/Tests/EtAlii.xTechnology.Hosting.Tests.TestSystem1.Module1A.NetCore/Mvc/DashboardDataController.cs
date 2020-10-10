namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Admin.Portal.NetCore
{
    using Microsoft.AspNetCore.Mvc;

    [Route("data")]
    public class DashboardDataController : AdminPortalController
    {
        [HttpGet]
        public string Get()
        {
            return "I'm Admin Data Controller. ";
        }
    }
}
