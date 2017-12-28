namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.AspNetCore
{
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminWebApiService : AspNetCoreServiceBase
    {
        public AdminWebApiService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseBranchWithServices(Port, "/admin/api",
                services =>
                {
                    //services.AddTransient<IHiService, ResourceService>();
                    services.AddMvc().ConfigureApplicationPartManager(manager =>
                    {
                        manager.FeatureProviders.Clear();
                        //manager.FeatureProviders.Add(new TypedControllerFeatureProvider<MyApiController>());
                    });
                },
                appBuilder =>
                {
                    appBuilder.Use(async (c, next) =>
                    {
                        await c.Response.WriteAsync("API!");
                    });
                    appBuilder.UseWelcomePage();//.UseDirectoryBrowser();
                    appBuilder.UseMvc();
                });


        }
    }
}
