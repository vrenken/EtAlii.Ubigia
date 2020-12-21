namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR
{
	using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Transport.SignalR;
    using EtAlii.Ubigia.Serialization;
    using EtAlii.xTechnology.Hosting;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.SignalR;

	public class UserSignalRService : ServiceBase
    {
        public UserSignalRService(
            IConfigurationSection configuration)
            : base(configuration)
        {
        }

        public override async Task Start()
        {
	        Status.Title = "Ubigia infrastructure user SignalR access";

	        Status.Description = "Starting...";
	        Status.Summary = "Starting Ubigia user SignalR services";

	        await base.Start().ConfigureAwait(false);

	        var sb = new StringBuilder();
	        sb.AppendLine("All OK. Ubigia user SignalR services are available on the address specified below.");
	        sb.AppendLine($"Address: {HostString}{PathString}");

	        Status.Description = "Running";
	        Status.Summary = sb.ToString();
        }

        public override async Task Stop()
        {
	        Status.Description = "Stopping...";
	        Status.Summary = "Stopping Ubigia user SignalR services";

	        await base.Stop().ConfigureAwait(false);

	        var sb = new StringBuilder();
	        sb.AppendLine("Finished providing Ubigia user SignalR services on the address specified below.");
	        sb.AppendLine($"Address: {HostString}{PathString}");

	        Status.Description = "Stopped";
	        Status.Summary = sb.ToString();
        }

        [SuppressMessage("Sonar Code Smell", "S4792:Configuring loggers is security-sensitive", Justification = "Safe to do so here.")]
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

		        .AddSignalRInfrastructureAuthentication(infrastructure)
		        .AddInfrastructureSerialization()

		        .AddRouting()
		        .AddCors()
		        .AddSignalR(options =>
		        {
                    options.AddFilter(new CorrelationServiceHubFilter(infrastructure.ContextCorrelator));

                    // SonarQube: Make sure that this logger's configuration is safe.
                    // As we only add the logging services this ought to be safe. It is when and how they are configured that matters.
			        if (Debugger.IsAttached)
			        {
				        options.EnableDetailedErrors = true;
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
