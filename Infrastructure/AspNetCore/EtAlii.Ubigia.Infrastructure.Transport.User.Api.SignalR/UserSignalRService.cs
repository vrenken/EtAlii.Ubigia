namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR
{
	using System.Diagnostics;
	using System.Linq;
	using EtAlii.Ubigia.Api.Transport;
	using EtAlii.Ubigia.Infrastructure.Transport.NetCore;
	using EtAlii.xTechnology.Hosting;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	public class UserSignalRService : ServiceBase
    {
        public UserSignalRService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
	        var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;
	        services
		        .AddSingleton(infrastructure.Spaces)
		        .AddSingleton(infrastructure.Accounts)
		        .AddSingleton(infrastructure.Roots)
		        .AddSingleton(infrastructure.Entries)
		        .AddSingleton(infrastructure.Properties)
		        .AddSingleton(infrastructure.Content)
		        .AddSingleton(infrastructure.ContentDefinition)

		        .AddInfrastructureAuthentication(infrastructure)
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
		        .AddHubOptions<ContentHub>(options =>
		        {
			        const long maximumReceiveMessageSizeInMegaByte = 1024 * 1024 * 2;
			        options.MaximumReceiveMessageSize = maximumReceiveMessageSizeInMegaByte;
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

			        endPoints.MapHub<AccountHub>(SignalRHub.Account);
			        endPoints.MapHub<SpaceHub>(SignalRHub.Space);

			        endPoints.MapHub<RootHub>(SignalRHub.Root);
			        endPoints.MapHub<EntryHub>(SignalRHub.Entry);
			        endPoints.MapHub<PropertiesHub>(SignalRHub.Property);
			        endPoints.MapHub<ContentHub>(SignalRHub.Content);
			        endPoints.MapHub<ContentDefinitionHub>(SignalRHub.ContentDefinition);
		        });
        }
    }
}
