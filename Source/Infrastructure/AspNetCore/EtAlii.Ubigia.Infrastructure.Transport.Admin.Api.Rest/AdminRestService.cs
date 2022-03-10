// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Transport.Rest;
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.Hosting.Service.Rest;
    using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.DependencyInjection;
    using EtAlii.xTechnology.Threading;
    using Microsoft.AspNetCore.Hosting;

    public class AdminRestService : INetworkService
    {
        public ServiceConfiguration Configuration { get; }

        private IContextCorrelator _contextCorrelator;

        public AdminRestService(ServiceConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services, IServiceProvider globalServices)
        {
            var infrastructure = globalServices.GetService<IInfrastructureService>()!.Infrastructure;
            _contextCorrelator = infrastructure.ContextCorrelator;

	        services
		        .AddSingleton(infrastructure.Storages)
		        .AddSingleton(infrastructure.Accounts)
		        .AddSingleton(infrastructure.Spaces)

		        .AddSingleton(infrastructure.Roots) // We wand the management portal to manage the roots as well.

                .AddSingleton(infrastructure.Options) // the service details are needed by the InformationController.

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

        public void ConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment)
        {
	        application
		        .UseRouting()
		        .UseAuthorization()
                .UseCorrelationIdsFromHeaders(_contextCorrelator)
		        .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
