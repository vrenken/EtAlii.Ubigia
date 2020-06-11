namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest
{
	using System.Linq;
	using EtAlii.Ubigia.Infrastructure.Transport.NetCore;
	using EtAlii.xTechnology.Hosting;
	using EtAlii.xTechnology.Hosting.Service.Rest;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	public class AdminRestService : ServiceBase
    {
        public AdminRestService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
	        var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;

	        services
		        .AddSingleton(infrastructure.Storages)
		        .AddSingleton(infrastructure.Accounts)
		        .AddSingleton(infrastructure.Spaces)

		        .AddSingleton(infrastructure.Roots) // We wand the management portal to manage the roots as well.

		        //.AddInfrastructureSimpleAuthentication(infrastructure)
		        .AddInfrastructureHttpContextAuthentication(infrastructure)
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
		        .UseEndpoints(endpoints => endpoints.MapControllers());
        }

    //     protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
    //     {
    //         var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;
    //
    //         applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.Admin.Api.Rest.BasePath,
    //             services =>
    //             {
	   //              services
		  //               .AddSingleton(infrastructure.Storages)
		  //               .AddSingleton(infrastructure.Accounts)
	   //                  .AddSingleton(infrastructure.Spaces)
    //
	   //                  .AddSingleton(infrastructure.Roots) // We wand the management portal to manage the roots as well.
    //
    //                     //.AddInfrastructureSimpleAuthentication(infrastructure)
    //                     .AddInfrastructureHttpContextAuthentication(infrastructure)
		  //               .AddInfrastructureSerialization()
    //
				// 		.AddMvcForTypedController<RestController>(options =>
		  //               {
			 //                options.EnableEndpointRouting = false;
			 //                options.InputFormatters.Clear();
				// 			options.InputFormatters.Add(new PayloadMediaTypeInputFormatter());
				// 			options.OutputFormatters.Clear();
				// 			options.OutputFormatters.Add(new PayloadMediaTypeOutputFormatter());
				// 		});
				// },
    //             appBuilder =>
    //             {
    //                 appBuilder.UseMvc();
    //             });
    //     }
    }
}
