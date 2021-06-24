// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Portal.Razor
{
    using System.Text;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.Hosting.Service.Rest;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class UserPortalControllerService : ServiceBase
    {
        public UserPortalControllerService(IConfigurationSection configuration) : base(configuration)
        {
        }

        public override async Task Start()
        {
            Status.Title = "Ubigia infrastructure user portal";

            Status.Description = "Starting...";
            Status.Summary = "Starting Ubigia user portal";

            await base.Start().ConfigureAwait(false);

            var sb = new StringBuilder();
            sb.AppendLine("All OK. Ubigia user portal is now available on the address specified below.");
            sb.AppendLine($"Address: {HostString}{PathString}");

            Status.Description = "Running";
            Status.Summary = sb.ToString();
        }

        public override async Task Stop()
        {
            Status.Description = "Stopping...";
            Status.Summary = "Stopping Ubigia user portal";

            await base.Stop().ConfigureAwait(false);

            var sb = new StringBuilder();
            sb.AppendLine("Finished providing Ubigia user portal on the address specified below.");
            sb.AppendLine($"Address: {HostString}{PathString}");

            Status.Description = "Stopped";
            Status.Summary = sb.ToString();
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services
                //    .AddSingleton<IAccountRepository>(infrastructure.Accounts)
                //    .AddSingleton<ISpaceRepository>(infrastructure.Spaces)
                //    .AddSingleton<IStorageRepository>(infrastructure.Storages)
                .AddControllers(options => options.EnableEndpointRouting = false)
                .AddTypedControllers<UserPortalController>();
        }

        protected override void ConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder
                .UseMvc()
                .UseWelcomePage();
        }

        // protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        // [
        //     applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.User.Portal.BaseUrl,
        //         services =>
        //         [
        //             services
        //             //    .AddSingleton<IAccountRepository>(infrastructure.Accounts)
        //             //    .AddSingleton<ISpaceRepository>(infrastructure.Spaces)
        //             //    .AddSingleton<IStorageRepository>(infrastructure.Storages)
        //                 .AddMvcForTypedController<UserPortalController>(options => options.EnableEndpointRouting = false)
        //         ],
        //         appBuilder =>
        //         [
        //             appBuilder.UseMvc()
        //             appBuilder.UseWelcomePage()
        //         ])
        // ]
    }
}
