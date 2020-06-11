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
        public AdminSignalRService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;
            services
                .AddSingleton(infrastructure.Accounts)
                .AddSingleton(infrastructure.Spaces)
                .AddSingleton(infrastructure.Storages)

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
                });
        }
            //
            //
            // protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
            // {
            //     var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;
            //
            //     applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.Admin.Api.SignalR.BasePath,
            //         services =>
            //         {
            //             services
            //                 .AddSingleton(infrastructure.Accounts)
            //                 .AddSingleton(infrastructure.Spaces)
            //                 .AddSingleton(infrastructure.Storages)
            //
            //                 .AddInfrastructureSimpleAuthentication(infrastructure)
            //                 .AddInfrastructureSerialization()
            //
            //                 .AddCors()
            //                 .AddSignalR(options => 
            //                 {
            //                     if (Debugger.IsAttached)
            //                     {
            //                         options.EnableDetailedErrors = Debugger.IsAttached;
            //                     }
            //                 })
            //                 .AddNewtonsoftJsonProtocol(options => SerializerFactory.Configure(options.PayloadSerializerSettings));
            //         },
            //         appBuilder =>
            //         {
            //             appBuilder
            //                 .UseCors(configuration =>
            //                 {
            //                     configuration.AllowAnyMethod();
            //                     configuration.AllowAnyHeader();
            //                     configuration.AllowAnyOrigin(); 
            //                 })
            //                 .UseRouting()
            //                 .UseEndpoints(endPoints =>
            //                 {
            //                     endPoints.MapHub<AuthenticationHub>(SignalRHub.Authentication);
            //
            //                     endPoints.MapHub<StorageHub>(SignalRHub.Storage);
            //                     endPoints.MapHub<SpaceHub>(SignalRHub.Space);
            //                     endPoints.MapHub<AccountHub>(SignalRHub.Account);
            //                 });
            //         });
            // }
    }
}
