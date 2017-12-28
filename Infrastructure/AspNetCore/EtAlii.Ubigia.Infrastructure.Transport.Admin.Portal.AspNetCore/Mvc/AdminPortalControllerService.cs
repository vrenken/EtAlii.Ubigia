namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.AspNetCore
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminPortalControllerService : AspNetCoreServiceBase
    {
        public AdminPortalControllerService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseBranchWithServices(Port, "/admin/portal",
                services =>
                {
                    //services.AddTransient<IHiService, AdminService>();
                    services.AddMvc().ConfigureApplicationPartManager(manager =>
                    {
                        manager.FeatureProviders.Clear();
                        manager.FeatureProviders.Add(new TypedControllerFeatureProvider<AdminPortalController>());
                    });
                },
                appBuilder =>
                {
                    appBuilder.Use(async (c, next) =>
                    {
                        if (c.Request.Path.ToString().Contains("foo"))
                        {
                            await c.Response.WriteAsync("bar!");
                        }
                        else
                        {
                            await next();
                        }
                    });
                    appBuilder.UseWelcomePage();

                    appBuilder.UseMvc();
                });
        }
    }
}
