﻿namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.NetCore
{
    using EtAlii.Ubigia.Infrastructure.Transport.NetCore;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;

    public class AdminPortalControllerService : NetCoreServiceBase
    {
        public AdminPortalControllerService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.Admin.Portal.BasePath,
                services =>
                {
                    services.AddMvcForTypedController<AdminPortalController>(options => options.EnableEndpointRouting = false);
                    //.AddRazorOptions(options =>
                    //[
                    //    options.FileProviders.Add(new EmbeddedFileProvider(GetType().Assembly, GetType().Namespace))
                    //])
                },
                appBuilder => appBuilder.UseWelcomePage().UseMvc());
        }
    }
}
