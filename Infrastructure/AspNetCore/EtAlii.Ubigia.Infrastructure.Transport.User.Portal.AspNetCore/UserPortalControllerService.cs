namespace EtAlii.Ubigia.Infrastructure.Transport.User.Portal.AspNetCore
{
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class UserPortalControllerService : AspNetCoreServiceBase
    {
        public UserPortalControllerService(IConfigurationSection configuration) : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseBranchWithServices(Port, "/user/portal",
                services =>
                {
                    services.AddMvc().ConfigureApplicationPartManager(manager =>
                    {
                        manager.FeatureProviders.Clear();
                    });
                },
                appBuilder =>
                {
                    appBuilder.Use(async (c, next) =>
                    {
                        await c.Response.WriteAsync("USER PORTAL!");
                    });
                    appBuilder.UseWelcomePage();//.UseDirectoryBrowser();
                    appBuilder.UseMvc();
                });
        }
    }
}
