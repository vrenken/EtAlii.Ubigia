// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal
{
    using System.Text;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.Hosting.Service.Rest;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminPortalControllerService : ServiceBase
    {
        public AdminPortalControllerService(IConfigurationSection configuration)
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
            services
                .AddControllers(options =>
                {
                    options.EnableEndpointRouting = false;
                })
                .AddTypedControllers<AdminPortalController>();
        }

        protected override void ConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder
                .UseRouting()
                .UseWelcomePage()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }

        // protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        // [
        //     applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.Admin.Portal.BasePath,
        //         services =>
        //         [
        //             services.AddMvcForTypedController<AdminPortalController>(options => options.EnableEndpointRouting = false)
        //             //.AddRazorOptions(options =>
        //             //[
        //             //    options.FileProviders.Add(new EmbeddedFileProvider(GetType().Assembly, GetType().Namespace))
        //             //])
        //         ],
        //         appBuilder => appBuilder.UseWelcomePage().UseMvc())
        // ]
    }
}
