﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest
{
    using System;
	using EtAlii.Ubigia.Infrastructure.Transport.Rest;
	using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.Hosting.Service.Rest;
    using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.DependencyInjection;
    using EtAlii.xTechnology.Threading;
    using Microsoft.AspNetCore.Hosting;

    public class UserRestService : INetworkService
    {
        /// <inheritdoc />
        public ServiceConfiguration Configuration { get; }

        private IContextCorrelator _contextCorrelator;

        public UserRestService(ServiceConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services, IServiceProvider globalServices)
        {
            var functionalContext = globalServices.GetService<IInfrastructureService>()!.Functional;
            _contextCorrelator = functionalContext.ContextCorrelator;

	        services
                .AddSingleton(functionalContext)
		        .AddSingleton(functionalContext.Storages)
                .AddSingleton(functionalContext.Spaces)
		        .AddSingleton(functionalContext.Accounts)

		        .AddSingleton(functionalContext.Roots)
		        .AddSingleton(functionalContext.Entries)
		        .AddSingleton(functionalContext.Properties)
		        .AddSingleton(functionalContext.Content)
		        .AddSingleton(functionalContext.ContentDefinition)

		        .AddAttributeBasedInfrastructureAuthorization()
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
