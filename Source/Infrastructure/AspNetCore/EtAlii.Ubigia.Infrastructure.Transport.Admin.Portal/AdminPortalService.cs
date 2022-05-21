// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal
{
    using System;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using Blazorise;
    using Blazorise.Bootstrap5;
    using Blazorise.Icons.FontAwesome;
    using Microsoft.AspNetCore.Hosting.StaticWebAssets;

    public class AdminPortalService : INetworkService
    {
        /// <inheritdoc />
        public ServiceConfiguration Configuration { get; }

        private readonly ILogger _logger = Log.ForContext<AdminPortalService>();

        public AdminPortalService(ServiceConfiguration configuration)
        {
            Configuration = configuration;
            _logger.Information("Instantiated {ServiceName}", nameof(AdminPortalService));
        }

        public void ConfigureServices(IServiceCollection services, IServiceProvider globalServices)
        {
            services
                .AddMvc()
                .AddApplicationPart(GetType().Assembly);

            services
                .AddRazorPages(options => options.RootDirectory = "/Shared");

            services
                .AddBlazorise(options =>
                {
                    options.Immediate = true;
                    //options.ChangeTextOnKeyPress = true; // optional
                })
                .AddBootstrap5Providers()
                .AddFontAwesomeIcons();

            services
                .AddServerSideBlazor(o => o.DetailedErrors = true)
                .AddHubOptions(options =>
                {
                    options.MaximumReceiveMessageSize = 1024 * 1024 * 100;
                });


            var infrastructureService = globalServices.GetService<IInfrastructureService>();
            if (infrastructureService != null) // The unit tests don't have access to the InfrastructureService.
            {
                services.AddSingleton(infrastructureService.Infrastructure);
            }
            services.AddSingleton<IConfiguration>(Configuration.Root);
            services.ConfigureOptions(typeof(UIConfigureOptions));
        }

        public void ConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            // The environment.ApplicationName needs to be set as the StaticWebAssetsLoader.UseStaticWebAssets relies on it.
            // Weird but true.
            environment.ApplicationName = typeof(AdminPortalService).Assembly.GetName().Name;
            StaticWebAssetsLoader.UseStaticWebAssets(environment, Configuration.Section);

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
            application.UseStaticFiles();
            application.UseRouting();

            application.UseAuthorization();
            application.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
