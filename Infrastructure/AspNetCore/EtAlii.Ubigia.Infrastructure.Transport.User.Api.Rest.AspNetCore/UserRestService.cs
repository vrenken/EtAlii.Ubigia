namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest.AspNetCore
{
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using EtAlii.xTechnology.Hosting.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class UserRestService : AspNetCoreServiceBase
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
	                    .AddSingleton<IStorageRepository>(infrastructure.Storages)
	                    .AddSingleton<IAccountRepository>(infrastructure.Accounts)
	                    .AddSingleton<ISpaceRepository>(infrastructure.Spaces)

						.AddSingleton<IRootRepository>(infrastructure.Roots)
                        .AddSingleton<IEntryRepository>(infrastructure.Entries)
                        .AddSingleton<IPropertiesRepository>(infrastructure.Properties)
                        .AddSingleton<IContentRepository>(infrastructure.Content)
                        .AddSingleton<IContentDefinitionRepository>(infrastructure.ContentDefinition)

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
