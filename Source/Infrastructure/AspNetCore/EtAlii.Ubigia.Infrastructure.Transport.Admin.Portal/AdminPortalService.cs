// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal
{
    using System.Text;
    using System.Threading.Tasks;
    using Blazorise;
    using Blazorise.Bootstrap;
    using Blazorise.Icons.FontAwesome;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class AdminPortalService : ServiceBase
    {
        public AdminPortalService(IConfigurationSection configuration)
            : base(configuration)
        {
        }

        public override async Task Start()
        {
            Status.Title = "Ubigia infrastructure admin portal";

            Status.Description = "Starting...";
            Status.Summary = "Starting Ubigia admin portal";

            await base.Start().ConfigureAwait(false);

            var sb = new StringBuilder();
            sb.AppendLine("All OK. Ubigia admin portal is now available on the address specified below.");
            sb.AppendLine($"Address: {HostString}{PathString}");

            Status.Description = "Running";
            Status.Summary = sb.ToString();
        }

        public override async Task Stop()
        {
            Status.Description = "Stopping...";
            Status.Summary = "Stopping Ubigia admin portal";

            await base.Stop().ConfigureAwait(false);

            var sb = new StringBuilder();
            sb.AppendLine("Finished providing Ubigia admin portal on the address specified below.");
            sb.AppendLine($"Address: {HostString}{PathString}");

            Status.Description = "Stopped";
            Status.Summary = sb.ToString();
        }

        protected override void ConfigureServices(IServiceCollection services)
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

        protected override void ConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            application
                .UseRouting()
                .UseWelcomePage();
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
