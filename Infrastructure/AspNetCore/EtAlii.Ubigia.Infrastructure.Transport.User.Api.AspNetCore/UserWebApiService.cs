namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.AspNetCore
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class UserWebApiService : AspNetCoreServiceBase
    {
        public UserWebApiService(IConfigurationSection configuration) : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseBranchWithServices(Port, "/user/api",
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
                        await c.Response.WriteAsync("USER API!");
                    });
                    appBuilder.UseWelcomePage();//.UseDirectoryBrowser();
                    appBuilder.UseMvc();
                });
        }
    }
}
