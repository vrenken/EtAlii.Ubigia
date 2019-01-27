namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.AspNetCore
{
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    public class HomeController : AdminPortalController
    {
        [HttpGet]
        public string Get()
        {
            return "This is my default action...";
        }
    }
}
