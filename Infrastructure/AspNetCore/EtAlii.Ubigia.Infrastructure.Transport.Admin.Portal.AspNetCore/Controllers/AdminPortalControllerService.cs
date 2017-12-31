namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.AspNetCore
{
    using System.Diagnostics;
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminPortalControllerService : AspNetCoreServiceBase
    {
        public AdminPortalControllerService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.Admin.Portal.BaseUrl,
                services => services.AddMvcForTypedController<AdminPortalController>(),
                appBuilder => appBuilder.UseWelcomePage().UseMvc());
        }
    }
}
