namespace EtAlii.Ubigia.Infrastructure.Transport.User.Portal.Razor
{
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
        // {
        //     applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.User.Portal.BaseUrl,
        //         services =>
        //         {
        //             services
        //             //    .AddSingleton<IAccountRepository>(infrastructure.Accounts)
        //             //    .AddSingleton<ISpaceRepository>(infrastructure.Spaces)
        //             //    .AddSingleton<IStorageRepository>(infrastructure.Storages)
        //                 .AddMvcForTypedController<UserPortalController>(options => options.EnableEndpointRouting = false);
        //         },
        //         appBuilder =>
        //         {
        //             appBuilder.UseMvc();
        //             appBuilder.UseWelcomePage();
        //         });
        // }
    }
}
