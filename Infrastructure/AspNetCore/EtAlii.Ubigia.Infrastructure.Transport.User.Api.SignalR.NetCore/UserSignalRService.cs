namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR.NetCore
{
	using System.Diagnostics;
	using System.Linq;
	using EtAlii.Ubigia.Api.Transport;
	using EtAlii.Ubigia.Infrastructure.Transport.NetCore;
	using EtAlii.xTechnology.Hosting;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	public class UserSignalRService : NetCoreServiceBase
    {
        public UserSignalRService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;

            applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.User.Api.SignalR.BaseUrl,
                services =>
                {
	                services
		                .AddSingleton(infrastructure.Spaces)
		                .AddSingleton(infrastructure.Accounts)
		                .AddSingleton(infrastructure.Roots)
		                .AddSingleton(infrastructure.Entries)
		                .AddSingleton(infrastructure.Properties)
		                .AddSingleton(infrastructure.Content)
		                .AddSingleton(infrastructure.ContentDefinition)

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

							routes.MapHub<AccountHub>(SignalRHub.Account);
							routes.MapHub<SpaceHub>(SignalRHub.Space);

							routes.MapHub<RootHub>(SignalRHub.Root);
                            routes.MapHub<EntryHub>(SignalRHub.Entry);
                            routes.MapHub<PropertiesHub>(SignalRHub.Property);
                            routes.MapHub<ContentHub>(SignalRHub.Content);
                            routes.MapHub<ContentDefinitionHub>(SignalRHub.ContentDefinition);
                        });
                });
        }
    }
}
