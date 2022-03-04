﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

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
    using Blazorise.Bootstrap;
    using Blazorise.Icons.FontAwesome;

    public class AdminPortalService : INetworkService
    {
        /// <inheritdoc />
        public Status Status { get; }

        /// <inheritdoc />
        public ServiceConfiguration Configuration { get; }

        private readonly ILogger _logger = Log.ForContext<AdminPortalService>();

        public AdminPortalService(ServiceConfiguration configuration, Status status)
        {
            Configuration = configuration;
            Status = status;
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
                    //options.Immediate = true;
                    options.ChangeTextOnKeyPress = true; // optional
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            services
                .AddServerSideBlazor()
                .AddHubOptions(options =>
                {
                    options.MaximumReceiveMessageSize = 1024 * 1024 * 100;
                });


            services.AddSingleton<IConfiguration>(Configuration.Root);
            services.AddSingleton<WeatherForecastService>();
            services.ConfigureOptions(typeof(UIConfigureOptions));
        }

        public void ConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            //environment.EnvironmentName = "Development";
            //environment.ApplicationName = GetType().Assembly.FullName;
            //environment.UseStaticWebAssets<AdminPortalService>();
            //environment.WebRootFileProvider = new ManifestEmbeddedFileProvider(GetType().Assembly, "wwwroot");

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
