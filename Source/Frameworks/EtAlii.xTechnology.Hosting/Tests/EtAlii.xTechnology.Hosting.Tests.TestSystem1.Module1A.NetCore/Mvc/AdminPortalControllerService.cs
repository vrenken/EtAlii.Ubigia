namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Admin.Portal.NetCore
{
    using System.Linq;
    using EtAlii.xTechnology.Hosting.Service.Rest;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminPortalControllerService : ServiceBase
    {
        public AdminPortalControllerService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void ConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            //applicationBuilder.UseBranchWithServices(Port, "/admin/portal",
            applicationBuilder.UseMvc();
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc(option => option.EnableEndpointRouting = false)
                .AddApplicationPart(GetType().Assembly)
                .ConfigureApplicationPartManager(manager =>
                {
                    
                    manager.FeatureProviders.Remove(manager.FeatureProviders.OfType<ControllerFeatureProvider>().Single());
                    manager.FeatureProviders.Add(new TypedControllerFeatureProvider<AdminPortalController>());
                });
        }
    }
}
