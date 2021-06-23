namespace EtAlii.xTechnology.Hosting.Tests.SignalRSystem
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class UserService : ServiceBase
    {
        public UserService(IConfigurationSection configuration)
            : base(configuration)
        {
        }

        protected override void ConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder
                .UseCors(builder =>
                {
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins($"http://{HostString}");
                })
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapHub<UserHub>(SignalRHub.User);
                });
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCors()
                .AddSignalR(options => options.EnableDetailedErrors = Debugger.IsAttached);
        }
    }
}

