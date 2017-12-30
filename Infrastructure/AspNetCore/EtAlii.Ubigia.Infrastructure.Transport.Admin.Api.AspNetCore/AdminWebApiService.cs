namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.AspNetCore
{
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminWebApiService : AspNetCoreServiceBase
    {
        public AdminWebApiService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;

            applicationBuilder.UseBranchWithServices(Port, "/admin/api",
                services =>
                {
                    services
                        .AddSingleton<IAccountRepository>(infrastructure.Accounts)
                        .AddSingleton<ISpaceRepository>(infrastructure.Spaces)
                        .AddSingleton<IStorageRepository>(infrastructure.Storages)
                        .AddMvcForTypedController<WebApiController>();
                },
                appBuilder => appBuilder.UseMvc());
        }

        protected override void Initialize(IAspNetCoreHost host, ISystem system, IModule[] moduleChain, out Status status)
        {
            base.Initialize(host, system, moduleChain, out status);
        }
    }
}
