﻿namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.AspNetCore
{
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;

    public class AdminPortalControllerService : AspNetCoreServiceBase
    {
        public AdminPortalControllerService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.Admin.Portal.BaseUrl,
                services =>
                {
                    services.AddMvcForTypedController<AdminPortalController>();
                    //.AddRazorOptions(options =>
                    //{
                    //    options.FileProviders.Add(new EmbeddedFileProvider(GetType().Assembly, GetType().Namespace));
                    //});
                },
                appBuilder => appBuilder.UseWelcomePage().UseMvc());
        }
    }
}
