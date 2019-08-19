namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest.NetCore
{
	using System.Linq;
	using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.Ubigia.Infrastructure.Transport.NetCore;
	using EtAlii.xTechnology.Hosting.NetCore;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	public class AdminRestService : NetCoreServiceBase
    {
        public AdminRestService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;

            applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.Admin.Api.Rest.BasePath,
                services =>
                {
	                services
		                .AddSingleton<IStorageRepository>(infrastructure.Storages)
		                .AddSingleton<IAccountRepository>(infrastructure.Accounts)
	                    .AddSingleton<ISpaceRepository>(infrastructure.Spaces)

	                    .AddSingleton<IRootRepository>(infrastructure.Roots) // We wand the management portal to manage the roots as well.

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
                appBuilder =>
                {
                    appBuilder.UseMvc();
                });
        }
    }
}
