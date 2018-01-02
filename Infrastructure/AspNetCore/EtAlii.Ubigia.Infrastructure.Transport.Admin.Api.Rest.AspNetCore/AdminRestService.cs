namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest.AspNetCore
{
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminRestService : AspNetCoreServiceBase
    {
        public AdminRestService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;

            applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.Admin.Api.Rest.BaseUrl,
                services =>
                {
                    services
                        .AddSingleton<IAccountRepository>(infrastructure.Accounts)
                        .AddSingleton<ISpaceRepository>(infrastructure.Spaces)
                        .AddSingleton<IStorageRepository>(infrastructure.Storages)
                        .AddMvcForTypedController<RestController>(options =>
                        {
                            //options.InputFormatters.Add();
                        });
                },
                appBuilder =>
                {
                    appBuilder.UseMvc();
                });
        }
    }
}
