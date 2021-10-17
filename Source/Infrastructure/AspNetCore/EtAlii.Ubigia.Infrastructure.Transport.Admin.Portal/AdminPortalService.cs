// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal
{
    using System;
    using System.Diagnostics;
    using Blazorise;
    using Blazorise.Bootstrap;
    using Blazorise.Icons.FontAwesome;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public class AdminPortalService : INetworkService
    {
        /// <inheritdoc />
        public Status Status { get; }

        /// <inheritdoc />
        public ServiceConfiguration Configuration { get; }

        private readonly ILogger _log = Log.ForContext<AdminPortalService>();

        public AdminPortalService(ServiceConfiguration configuration, Status status)
        {
            Configuration = configuration;
            Status = status;
            _log.Information("Instantiated {ServiceName}", nameof(AdminPortalService));
        }

        public void ConfigureServices(IServiceCollection services, IServiceProvider globalServices)
        {
            services.AddMvc();

            services
                .AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = true;
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            services
                .AddSingleton<IConfiguration>(Configuration.Root)
                .Configure<RazorPagesOptions>(options => options.RootDirectory = "/Pages")

                .AddRazorPages()//options => options.RootDirectory = "/Pages") // This is where the _Host.cshtml can be found.
                .ConfigureApplicationPartManager(p =>
                {
                    p.ApplicationParts.Add(new AssemblyPart(GetType().Assembly));
                });
            services
                .AddServerSideBlazor()
                .AddHubOptions(options =>
                {
                    options.MaximumReceiveMessageSize = 1024 * 1024 * 100;
                });
        }

        public void ConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            environment.ApplicationName = GetType().Assembly.FullName;
            application
                .UseHsts();
                // .UseWelcomePage();

            if (environment.IsDevelopment() || Debugger.IsAttached)
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
                 endpoints.MapControllerRoute(name: "default", pattern: "{controller}/{action}");
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
