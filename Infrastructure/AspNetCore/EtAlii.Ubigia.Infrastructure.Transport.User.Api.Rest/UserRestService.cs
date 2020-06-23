namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest
{
	using System.Linq;
	using EtAlii.Ubigia.Infrastructure.Transport.NetCore;
	using EtAlii.xTechnology.Hosting;
	using EtAlii.xTechnology.Hosting.Service.Rest;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	public class UserRestService : ServiceBase
    {
        public UserRestService(IConfigurationSection configuration) : base(configuration)
        {
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
	        var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;

	        services
		        .AddSingleton(infrastructure.Storages)
		        .AddSingleton(infrastructure.Accounts)
		        .AddSingleton(infrastructure.Spaces)

		        .AddSingleton(infrastructure.Roots)
		        .AddSingleton(infrastructure.Entries)
		        .AddSingleton(infrastructure.Properties)
		        .AddSingleton(infrastructure.Content)
		        .AddSingleton(infrastructure.ContentDefinition)

		        .AddAttributeBasedInfrastructureAuthorization(infrastructure)
		        .AddInfrastructureSerialization()

		        .AddControllers(options =>
		        {
			        options.EnableEndpointRouting = false;
			        options.InputFormatters.Clear();
			        options.InputFormatters.Add(new PayloadMediaTypeInputFormatter());
			        options.OutputFormatters.Clear();
			        options.OutputFormatters.Add(new PayloadMediaTypeOutputFormatter());
		        })
		        .AddTypedControllers<RestController>();
        }

        protected override void ConfigureApplication(IApplicationBuilder applicationBuilder)
        {
	        applicationBuilder
		        .UseRouting()
		        .UseAuthorization()
		        .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
