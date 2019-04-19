namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.SignalR.AspNetCore
{
    using System.Diagnostics;
    using System.Linq;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using EtAlii.xTechnology.Hosting.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminSignalRService : AspNetCoreServiceBase
    {
        public AdminSignalRService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;

            applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.Admin.Api.SignalR.BasePath,
                services =>
                {
                    services
                        .AddSingleton<IAccountRepository>(infrastructure.Accounts)
                        .AddSingleton<ISpaceRepository>(infrastructure.Spaces)
                        .AddSingleton<IStorageRepository>(infrastructure.Storages)

                        .AddInfrastructureSimpleAuthentication(infrastructure)
                        .AddInfrastructureSerialization()

                        .AddCors()
                        .AddSignalR(options => 
                        {
                            if (Debugger.IsAttached)
                            {
                                options.EnableDetailedErrors = Debugger.IsAttached;
                            }
                        })
                        .AddJsonProtocol(options => SerializerFactory.Configure(options.PayloadSerializerSettings));
                },
                appBuilder =>
                {
                    appBuilder
                        .UseCors(configuration =>
                        {
                            configuration.AllowAnyMethod();
                            configuration.AllowAnyHeader();
                            configuration.AllowAnyOrigin(); 
                        })
                        .UseSignalR(routes =>
                        {
                            routes.MapHub<AuthenticationHub>(SignalRHub.Authentication);

                            routes.MapHub<StorageHub>(SignalRHub.Storage);
                            routes.MapHub<SpaceHub>(SignalRHub.Space);
                            routes.MapHub<AccountHub>(SignalRHub.Account);
						});
                });
        }
    }
}
