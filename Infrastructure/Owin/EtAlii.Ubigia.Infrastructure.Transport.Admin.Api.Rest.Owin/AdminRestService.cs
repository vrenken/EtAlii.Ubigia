namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest.Owin
{
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin;
    using EtAlii.xTechnology.Hosting;
    using global::Owin;
    using Microsoft.Extensions.Configuration;
    //using Microsoft.Extensions.DependencyInjection;

    public class AdminRestService : OwinServiceBase
	{
        public AdminRestService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IAppBuilder applicationBuilder)
        {
            var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;

	  //      applicationBuilder.Map(AbsoluteUri.Admin.Api.Rest.BaseUrl, builder =>
	  //      {
	  //      });
			//applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.Admin.Api.Rest.BaseUrl,
   //             services =>
   //             {
   //                 services
   //                     .AddSingleton<IAccountRepository>(infrastructure.Accounts)
   //                     .AddSingleton<ISpaceRepository>(infrastructure.Spaces)
   //                     .AddSingleton<IStorageRepository>(infrastructure.Storages)
   //                     .AddMvcForTypedController<RestController>(options =>
   //                     {
   //                         //options.InputFormatters.Add();
   //                     });
   //             },
   //             appBuilder =>
   //             {
   //                 appBuilder.UseMvc();
   //             });
        }
    }
}
