// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest
{
	using System.Linq;
    using System.Text;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Infrastructure.Transport.Rest;
	using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.Hosting.Service.Rest;
    using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
    using EtAlii.xTechnology.Threading;

	public class UserRestService : ServiceBase
    {
        private IContextCorrelator _contextCorrelator;

        public UserRestService(
            IConfigurationSection configuration) : base(configuration)
        {
        }

        public override async Task Start()
        {
	        Status.Title = "Ubigia infrastructure user REST access";

	        Status.Description = "Starting...";
	        Status.Summary = "Starting Ubigia user REST services";

	        await base.Start().ConfigureAwait(false);

	        var sb = new StringBuilder();
	        sb.AppendLine("All OK. Ubigia user REST services are available on the address specified below.");
	        sb.AppendLine($"Address: {HostString}{PathString}");

	        Status.Description = "Running";
	        Status.Summary = sb.ToString();
        }

        public override async Task Stop()
        {
	        Status.Description = "Stopping...";
	        Status.Summary = "Stopping Ubigia user REST services";

	        await base.Stop().ConfigureAwait(false);

	        var sb = new StringBuilder();
	        sb.AppendLine("Finished providing Ubigia user REST services on the address specified below.");
	        sb.AppendLine($"Address: {HostString}{PathString}");

	        Status.Description = "Stopped";
	        Status.Summary = sb.ToString();
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
	        var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;
            _contextCorrelator = infrastructure.ContextCorrelator;

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
		        .AddControllers()
		        .AddInfrastructureSerialization()
		        .AddMvcOptions(options =>
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
                .UseCorrelationIdsFromHeaders(_contextCorrelator)
		        .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
