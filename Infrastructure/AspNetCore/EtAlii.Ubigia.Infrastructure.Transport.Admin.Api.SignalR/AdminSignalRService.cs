namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.SignalR
{
    using System.Diagnostics;
    using System.Linq;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Transport.NetCore;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminSignalRService : ServiceBase
    {
        private readonly IConfigurationDetails _configurationDetails;

        public AdminSignalRService(IConfigurationSection configuration, IConfigurationDetails configurationDetails) 
            : base(configuration)
        {
            _configurationDetails = configurationDetails;
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;
            services
                .AddSingleton(infrastructure.Accounts)
                .AddSingleton(infrastructure.Spaces)
                .AddSingleton(infrastructure.Storages)
                .AddSingleton(infrastructure.Information)

                .AddSingleton(_configurationDetails) // the configuration details are needed by the InformationController.

                .AddInfrastructureSimpleAuthentication(infrastructure)
                .AddInfrastructureSerialization()

                .AddRouting()
                .AddCors()
                .AddSignalR(options => 
                {
                    if (Debugger.IsAttached)
                    {
                        options.EnableDetailedErrors = Debugger.IsAttached;
                    }
                })
                .AddNewtonsoftJsonProtocol(options => SerializerFactory.Configure(options.PayloadSerializerSettings));
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
                .UseEndpoints(endPoints =>
                {
                    endPoints.MapHub<AuthenticationHub>(SignalRHub.Authentication);

                    endPoints.MapHub<StorageHub>(SignalRHub.Storage);
                    endPoints.MapHub<SpaceHub>(SignalRHub.Space);
                    endPoints.MapHub<AccountHub>(SignalRHub.Account);
                    endPoints.MapHub<InformationHub>(SignalRHub.Information);
                });
        }
    }
}
