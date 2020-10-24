namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Admin.Api.NetCore
{
    using System.Diagnostics;
    using EtAlii.xTechnology.Hosting.Service.Rest;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminWebApiService : ServiceBase
    {
        public AdminWebApiService(IConfigurationSection configuration) : base(configuration)
        {
        }

        protected override void ConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    if (Debugger.IsAttached)
                    {
                        endpoints.AddDebugRouter();
                    }
                    endpoints.MapControllers();
                });

        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<AdminController>();
            services
                .AddControllers()
                .AddTypedControllers<AdminController>();
        }
    }
}
