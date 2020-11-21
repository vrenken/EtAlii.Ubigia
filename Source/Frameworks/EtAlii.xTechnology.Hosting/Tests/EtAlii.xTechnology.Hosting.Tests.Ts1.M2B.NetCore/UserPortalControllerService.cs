namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.Portal.NetCore
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class UserPortalControllerService : ServiceBase
    {
        public UserPortalControllerService(IConfigurationSection configuration) : base(configuration)
        {
        }

        protected override void ConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            //applicationBuilder.UseBranchWithServices(Port, "/user/portal",
            applicationBuilder.Use(async (c, _) =>
            {
                await c.Response.WriteAsync("USER PORTAL!").ConfigureAwait(false);
            });
            applicationBuilder.UseWelcomePage();
            applicationBuilder.UseMvc();
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false).ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Clear();
            });
        }
    }
}
