namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest.NetCore
{
	using System.Linq;
	using EtAlii.Ubigia.Infrastructure.Transport.NetCore;
	using EtAlii.xTechnology.Hosting;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	public class UserRestService : NetCoreServiceBase
    {
        public UserRestService(IConfigurationSection configuration) : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;

            applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.User.Api.Rest.BaseUrl,
                services =>
                {
                    services
	                    .AddSingleton(infrastructure.Storages)
	                    .AddSingleton(infrastructure.Accounts)
	                    .AddSingleton(infrastructure.Spaces)

						.AddSingleton(infrastructure.Roots)
                        .AddSingleton(infrastructure.Entries)
                        .AddSingleton(infrastructure.Properties)
                        .AddSingleton(infrastructure.Content)
                        .AddSingleton(infrastructure.ContentDefinition)

	                    //.AddInfrastructureSimpleAuthentication(infrastructure)
	                    .AddInfrastructureHttpContextAuthentication(infrastructure)
		                .AddInfrastructureSerialization()

		                .AddMvcForTypedController<RestController>(options =>
		                {
			                options.InputFormatters.Clear();
							options.InputFormatters.Add(new PayloadMediaTypeInputFormatter());
			                options.OutputFormatters.Clear();
							options.OutputFormatters.Add(new PayloadMediaTypeOutputFormatter());
						});
				},
                appBuilder => appBuilder.UseMvc());
        }
    }
}
