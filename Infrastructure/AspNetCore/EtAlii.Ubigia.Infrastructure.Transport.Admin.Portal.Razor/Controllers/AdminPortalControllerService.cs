namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.Razor
{
    // using EtAlii.Ubigia.Infrastructure.Transport.NetCore;
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
        // {
        //     applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.Admin.Portal.BasePath,
        //         services =>
        //         {
        //             services.AddMvcForTypedController<AdminPortalController>(options => options.EnableEndpointRouting = false);
        //             //.AddRazorOptions(options =>
        //             //[
        //             //    options.FileProviders.Add(new EmbeddedFileProvider(GetType().Assembly, GetType().Namespace))
        //             //])
        //         },
        //         appBuilder => appBuilder.UseWelcomePage().UseMvc());
        // }
    }
}
