namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Admin.Api.NetCore
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminSignalRService : ServiceBase
    {
        public AdminSignalRService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }
        
        protected override void ConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder
                .UseCors(builder =>
                {
                    builder
                        .AllowAnyOrigin() 
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins($"http://{HostString}");
                })
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapHub<AdminHub>($"/{nameof(AdminHub)}"));
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddRouting()
                .AddCors()
                .AddSignalR(options =>
                 {
                     options.EnableDetailedErrors = Debugger.IsAttached;
                 })
                .AddJsonProtocol();
        }
    }
}
