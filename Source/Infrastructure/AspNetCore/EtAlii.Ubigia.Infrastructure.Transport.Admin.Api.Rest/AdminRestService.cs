// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest
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
    using Microsoft.AspNetCore.Hosting;

    public class AdminRestService : ServiceBase
    {
	    private readonly IConfigurationDetails _configurationDetails;
        private IContextCorrelator _contextCorrelator;

        public AdminRestService(
            IConfigurationSection configuration,
            IConfigurationDetails configurationDetails)
            : base(configuration)
        {
            _configurationDetails = configurationDetails;
        }

	    public override async Task Start()
	    {
		    Status.Title = "Ubigia infrastructure admin REST access";

		    Status.Description = "Starting...";
		    Status.Summary = "Starting Ubigia admin REST services";

		    await base.Start().ConfigureAwait(false);

		    var sb = new StringBuilder();
		    sb.AppendLine("All OK. Ubigia admin REST services are available on the address specified below.");
		    sb.AppendLine($"Address: {HostString}{PathString}");

		    Status.Description = "Running";
		    Status.Summary = sb.ToString();
	    }

	    public override async Task Stop()
	    {
		    Status.Description = "Stopping...";
		    Status.Summary = "Stopping Ubigia admin REST services";

		    await base.Stop().ConfigureAwait(false);

		    var sb = new StringBuilder();
		    sb.AppendLine("Finished providing Ubigia admin REST services on the address specified below.");
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

		        .AddSingleton(infrastructure.Roots) // We wand the management portal to manage the roots as well.

		        .AddSingleton(_configurationDetails) // the configuration details are needed by the InformationController.

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

        protected override void ConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment)
        {
	        application
		        .UseRouting()
		        .UseAuthorization()
                .UseCorrelationIdsFromHeaders(_contextCorrelator)
		        .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
