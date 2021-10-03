// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal
{
    using System;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public class AdminPortalService : INetworkService
    {
        public ServiceConfiguration Configuration { get; }
        private readonly ILogger _log = Log.ForContext<AdminPortalService>();

        public AdminPortalService(ServiceConfiguration configuration)
        {
            Configuration = configuration;
            _log.Information("Instantiated {ServiceName}", nameof(AdminPortalService));
        }

        public void ConfigureServices(IServiceCollection services, IServiceProvider globalServices)
        {
            services.AddMvc();
            //
            // services
            //     .AddBlazorise(options =>
            //     {
            //         options.ChangeTextOnKeyPress = true;
            //     })
            //     .AddBootstrapProviders()
            //     .AddFontAwesomeIcons();
            //
            // services.AddRazorPages(options => options.RootDirectory = "/Shared"); // This is where the _Host.cshtml can be found.
            // services
            //     .AddServerSideBlazor()
            //     .AddHubOptions(options =>
            //     {
            //         options.MaximumReceiveMessageSize = 1024 * 1024 * 100;
            //     });
        }

        public void ConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            application
                .UseRouting()
                .UseWelcomePage()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            return;

            if (environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }
            else
            {
                application.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                application.UseHsts();
            }

            //application.UseHttpsRedirection();
            application.UseStaticFiles();

            application.UseRouting();

            application.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            // builder
            //     .UseRouting()
            //     .UseWelcomePage()
            //     .UseEndpoints(endpoints =>
            //     {
            //         endpoints.MapControllers();
            //     });
        }
    }
}
